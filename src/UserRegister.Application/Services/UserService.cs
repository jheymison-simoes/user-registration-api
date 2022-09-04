using System.Globalization;
using System.Resources;
using AutoMapper;
using UserRegister.Business.EntityModels;
using UserRegister.Business.Interfaces.Repositories;
using UserRegister.Business.Interfaces.Services;
using UserRegister.Business.Models;
using UserRegister.Business.Response;

namespace UserRegister.Application.Services;

public class UserService : BaseService, IUserService
{
    #region Inject Configs
    // private readonly ResourceSet _resourceSet;
    // private readonly ResourceManager _resourceManager;
    // private readonly CultureInfo _cultureInfo;
    // private readonly IMapper _mapper;
    #endregion

    #region Inject Repositories
    private readonly IUserRepository _userRepository;
    #endregion

    #region Inject Validators
    private readonly CreateUserValidator _createUserValidator;
    #endregion
    
    // public UserService(
    //     ResourceManager resourceManager,
    //     CultureInfo cultureInfo,
    //     IMapper mapper,
    //     IUserRepository userRepository, CreateUserValidator createUserValidator)
    // {
    //     _resourceSet = resourceManager.GetResourceSet(cultureInfo, true, true);
    //     _resourceManager = resourceManager;
    //     _cultureInfo = cultureInfo;
    //     _userRepository = userRepository;
    //     _createUserValidator = createUserValidator;
    //     _mapper = mapper;
    // }
    
    public UserService(
        ResourceManager resourceManager,
        CultureInfo cultureInfo,
        IMapper mapper,
        IUserRepository userRepository, 
        CreateUserValidator createUserValidator) 
        : base(resourceManager, cultureInfo, mapper)
    {
        _userRepository = userRepository;
        _createUserValidator = createUserValidator;
    }


    public async Task<UserResponse> CreateUser(CreateUserModel createUser)
    {
        await _createUserValidator.Validate(createUser, ResourceManager, CultureInfo);
        var userExisting = await _userRepository.Get(u => u.Cpf == createUser.Cpf);
        if (userExisting is not null) ResponseError("USER-USER_EXISTING_BY_CPF", userExisting.Cpf);

        var newUser = Mapper.Map<User>(createUser);
        _userRepository.Add(newUser);
        
        await _userRepository.SaveChanges();
        return Mapper.Map<UserResponse>(newUser);
    }
}