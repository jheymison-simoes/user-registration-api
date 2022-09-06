using System.Globalization;
using System.Resources;
using AutoMapper;
using Microsoft.Extensions.Options;
using UserRegister.Business.Exceptions;
using UserRegister.Business.Interfaces.Services;
using UserRegister.Business.Interfaces.Services.Clients;
using UserRegister.Business.Models;
using UserRegister.Business.Models.Clients;

namespace UserRegister.Application.Services.Clients;

public class ViaCepService : BaseService, IViaCepService
{
    #region Inject Config
    private readonly IRequestProvider _requestProvider;
    private readonly AppSettings _appSettins;
    #endregion
    
    public ViaCepService(
        ResourceManager resourceManager, 
        CultureInfo cultureInfo, 
        IMapper mapper,
        IRequestProvider requestProvider, 
        IOptions<AppSettings> appSettins) 
        : base(resourceManager, cultureInfo, mapper)
    {
        _requestProvider = requestProvider;
        _appSettins = appSettins.Value;
        _requestProvider.SetBaseUrl(_appSettins.ViaCepBaseUrl);
    }

    // Daria para usar também o REFIT para fazer o request nio ViaCep
    public async Task<ViaCepResponseModel> GetAddressByPostalCode(string cep)
    {
        try
        {
            return await _requestProvider.GetAsync<ViaCepResponseModel>($"/{cep}/json/");;
        }
        catch (Exception ex)
        {
            throw new CustomException(ex.Message);
        }
    }
}