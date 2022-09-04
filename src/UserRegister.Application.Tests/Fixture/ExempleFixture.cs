using System;
using System.Resources;
using AutoMapper;
using UserRegister.Api.Configuration;
using UserRegister.Application.Services;
using UserRegister.Application.Tests.Utils;
using UserRegister.Business.Interfaces.Repositories;
using Moq;
using Xunit;

namespace UserRegister.Application.Tests.Fixture;

[CollectionDefinition(nameof(ExempleCollection))]
public class ExempleCollection : ICollectionFixture<ExempleFixture>
{
}

public class ExempleFixture : IDisposable
{
    public ExempleService ExempleService;
    public ResourceManager ResourceManager;
    public IMapper Mapper;

    public void GenerateExempleService()
    {
        ResourceManager = new ResourceManager(typeof(Api.Resource.ApiResource));
        Mapper = MapperTests.Mapping<AutoMapperConfiguration>();
        
        ExempleService = new ExempleService();
    }
    
    public void Dispose()
    {
    }
}