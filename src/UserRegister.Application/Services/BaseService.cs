using System.Globalization;
using System.Resources;
using AutoMapper;
using Microsoft.Extensions.Options;
using UserRegister.Business.Exceptions;
using UserRegister.Business.Models;
using UserRegister.Business.Utils;

namespace UserRegister.Application.Services;

public class BaseService
{
    protected readonly ResourceSet ResourceSet;
    protected readonly ResourceManager ResourceManager;
    protected readonly CultureInfo CultureInfo;
    protected readonly IMapper Mapper;

    public BaseService(
        ResourceManager resourceManager, 
        CultureInfo cultureInfo, 
        IMapper mapper)
    {
        ResourceSet = resourceManager.GetResourceSet(cultureInfo, true, true);
        ResourceManager = resourceManager;
        CultureInfo = cultureInfo;
        Mapper = mapper;
    }
    
    public void ResponseError(string name, params object[] parameters)
    {
        var messageError = parameters.Length > default(int) 
            ? ResourceSet.GetString(name)!.ResourceFormat(parameters) 
            : ResourceSet.GetString(name);
        
        throw new CustomException(messageError);
    }
}