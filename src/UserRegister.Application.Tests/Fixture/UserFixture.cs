using System;
using System.Globalization;
using System.Resources;
using AutoMapper;
using Bogus;
using Moq;
using UserRegister.Api.Configuration;
using UserRegister.Application.Services;
using UserRegister.Application.Tests.Utils;
using UserRegister.Business.EntityModels;
using UserRegister.Business.Interfaces.Repositories;
using UserRegister.Business.Interfaces.Services.Clients;
using UserRegister.Business.Models;
using UserRegister.Data.Repositories;
using Xunit;

namespace UserRegister.Application.Tests.Fixture;

[CollectionDefinition(nameof(UserCollection))]
public class UserCollection : ICollectionFixture<UserFixture>
{
}

public class UserFixture : IDisposable
{
    public UserService UserService;
    public Mock<IUserRepository> UserRepository;
    public Mock<IViaCepService> ViaCepService;
    public CreateUserValidator CreateUserValidator;
    public UserValidador UserValidador;

    public ResourceManager ResourceManager;
    public IMapper Mapper;
    
    public void GenerateUserService()
    {
        UserRepository = new Mock<IUserRepository>();
        ViaCepService = new Mock<IViaCepService>();
        ResourceManager = new ResourceManager(typeof(Api.Resource.ApiResource));
        Mapper = MapperTests.Mapping<AutoMapperConfiguration>();
        var culture = CultureInfo.GetCultureInfo("pt-BR");
        CreateUserValidator = new CreateUserValidator(ResourceManager, culture);
        UserValidador = new UserValidador(ResourceManager, culture);
        
        UserService = new UserService(
            ResourceManager,
            culture,
            Mapper,
            CreateUserValidator,
            UserValidador,
            UserRepository.Object,
            ViaCepService.Object
        );
    }

    public CreateUserModel CreateInvalidUser()
    {
        return new Faker<CreateUserModel>("pt_BR")
            .CustomInstantiator(f => new CreateUserModel()
            {
                Name = f.Name.FullName(),
                Cpf = null,
                LegalPerson = false,
                Email = f.Internet.Email()
            });
    }
    
    public void Dispose()
    {
    }
}