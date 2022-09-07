using System.Globalization;
using System.Resources;
using FluentAssertions;
using FluentValidation;
using FluentValidation.TestHelper;
using Moq;
using UserRegister.Application.Tests.Fixture;
using UserRegister.Business.Exceptions;
using UserRegister.Business.Models;
using Xunit;

namespace UserRegister.Application.Tests.Services;

[Collection(nameof(UserCollection))]
public class UserServiceTests
{
    private readonly UserFixture _userFixture;

    public UserServiceTests(UserFixture userFixture)
    {
        _userFixture = userFixture;
    }
    
    [Fact(DisplayName = "Check if any property is null or invalid")]
    [Trait("Category", "User Service")]
    public async void User_NotCreate_AnyPropertyIsNullOrInvalid()
    {
        _userFixture.GenerateUserService();
        var invalidUser = _userFixture.CreateInvalidUser();
        var result = _userFixture.CreateUserValidator.TestValidate(invalidUser);
        result.IsValid.Should().BeFalse();
    }
}