using System;
using System.Collections.Generic;
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
using UserRegister.Business.Models.Clients;
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

    // public CreateUserModel CreateInvalidUser()
    // {
    //     return new Faker<CreateUserModel>("pt_BR")
    //         .CustomInstantiator(f => new CreateUserModel()
    //         {
    //             Name = f.Name.FullName(),
    //             Cpf = null,
    //             LegalPerson = false,
    //             Email = f.Internet.Email()
    //         });
    // }
    
    public CreateUserModel CreateValidUser()
    {
        return new Faker<CreateUserModel>("pt_BR")
            .CustomInstantiator(f => new CreateUserModel()
            {
                Name = f.Name.FullName(),
                Cpf = f.Random.ReplaceNumbers("###########"),
                LegalPerson = false,
                Email = f.Internet.Email(),
                Address = new CreateUserAddressModel()
                {
                    City = f.Address.City(),
                    District = f.Address.FullAddress(),
                    Number = f.Address.BuildingNumber(),
                    State = f.Address.State(),
                    Street = f.Address.StreetName(),
                    PostalCode = f.Address.ZipCode("########")
                },
                UserPhones = new List<CreateUserPhoneModel>()
                {
                    new CreateUserPhoneModel()
                    {
                        Ddd = f.Random.Int(10, 50).ToString(),
                        NumberPhone = f.Phone.PhoneNumber("#########")
                    }
                }
            });
    }

    public ViaCepResponseModel CreateViaCepResponseModel()
    {
        return new Faker<ViaCepResponseModel>("pt_BR")
            .CustomInstantiator(f => new ViaCepResponseModel()
            {
                Bairro = f.Address.FullAddress(),
                Cep = f.Address.ZipCode("########"),
                Complemento = f.Address.FullAddress(),
                Ddd = f.Random.ReplaceNumbers("##"),
                Gia = f.Random.String2(1, 40),
                Ibge = f.Random.ReplaceNumbers("####"),
                Localidade = f.Address.City(),
                Logradouro = f.Address.StreetAddress(),
                Siafi = f.Random.ReplaceNumbers("####"),
                Uf = f.Random.Replace("??")
            });
    }

    public string GetMessageResource(string resouceName, params object[] args)
    {
        return args.Length == 0 
            ? ResourceManager.GetString(resouceName)! 
            : string.Format(ResourceManager.GetString(resouceName)!, args);
    }

    public void Dispose()
    {
    }
}