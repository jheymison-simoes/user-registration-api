using System.Globalization;
using System.Resources;
using AutoMapper;
using UserRegister.Business.EntityModels;
using UserRegister.Business.Interfaces.Repositories;
using UserRegister.Business.Interfaces.Services;
using UserRegister.Business.Interfaces.Services.Clients;
using UserRegister.Business.Models;
using UserRegister.Business.Models.Clients;
using UserRegister.Business.Response;

namespace UserRegister.Application.Services;

public class UserService : BaseService, IUserService
{
    #region Inject Configs
    #endregion

    #region Inject Repositories
    private readonly IUserRepository _userRepository;
    #endregion

    #region Inject Services
    private readonly IViaCepService _viaCepService;
    #endregion

    #region Inject Validators
    private readonly CreateUserValidator _createUserValidator;
    private readonly UserValidador _userValidador;
    #endregion
    
    public UserService(
        ResourceManager resourceManager,
        CultureInfo cultureInfo,
        IMapper mapper,
        CreateUserValidator createUserValidator,
        UserValidador userValidador,
        IUserRepository userRepository, 
        IViaCepService viaCepService) 
        : base(resourceManager, cultureInfo, mapper)
    {
        _userRepository = userRepository;
        _viaCepService = viaCepService;
        _userValidador = userValidador;
        _createUserValidator = createUserValidator;
    }
    
    public async Task<UserResponse> CreateUser(CreateUserModel createUser)
    {
        await _createUserValidator.Validate(createUser, ResourceManager, CultureInfo);
        await CheckExistingUser(createUser.Cpf, createUser.Email);
        await CheckExistingPhone(createUser.UserPhones);
        var addressViaCep = await GetAndValidateAddress(createUser.Address.PostalCode);
        
        var newUser = Mapper.Map<User>(createUser);
        newUser.Address.City = addressViaCep.Localidade;
        newUser.Address.District = string.IsNullOrWhiteSpace(addressViaCep.Bairro)
            ? newUser.Address.District
            : addressViaCep.Bairro;
        newUser.Address.State = addressViaCep.Uf;

        await _userValidador.Validate(newUser, ResourceManager, CultureInfo);
        
        _userRepository.Add(newUser);
        await _userRepository.SaveChanges();
        
        return Mapper.Map<UserResponse>(newUser);
    }
    
    #region Private Methods
    private async Task CheckExistingUser(string cpf, string email)
    {
        var userExisting = await _userRepository.Get(u => u.Cpf == cpf || u.Email == email);
        if (userExisting is null) return; 
        ResponseError("USER-USER_EXISTING_BY_CPF_OR_EMAIL", userExisting.Cpf, userExisting.Email);
    }
    
    private async Task CheckExistingPhone(List<CreateUserPhoneModel> userPhones)
    {
        var phonesExistings = new List<string>();
        foreach (var phone in userPhones)
        {
            var existingPhone = await _userRepository.ExistingPhone(phone.Ddd, phone.NumberPhone);
            if (!existingPhone) continue; 
            phonesExistings.Add($"({phone.Ddd}) {phone.NumberPhone}");
        }
        if (!phonesExistings.Any()) return;
        ResponseError("USER-USERPHONE_EXISTINGS", string.Join(", ", phonesExistings));
    }

    private async Task<ViaCepResponseModel> GetAndValidateAddress(string postalCode)
    {
        var getAddressByCep = await _viaCepService.GetAddressByPostalCode(postalCode);
        if (getAddressByCep is null) ResponseError("USER-ADDRESS-NOT_FOUND_ADDRESS_BY_POSTAL_CODE", postalCode);
        return getAddressByCep;
    }
    #endregion
}