using System;
using System.Linq;
using System.Linq.Expressions;
using FluentAssertions;
using Moq;
using UserRegister.Application.Tests.Fixture;
using UserRegister.Business.EntityModels;
using UserRegister.Business.Exceptions;
using UserRegister.Business.Models.Clients;
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
    
    [Fact(DisplayName = "Do not create user if any property is null or invalid")]
    [Trait("Category", "User Service")]
    public async void User_NotCreate_AnyPropertyIsNullOrInvalid()
    {
        // Arrange
        _userFixture.GenerateUserService();
        var invalidUser = _userFixture.CreateValidUser();
        invalidUser.Cpf = null;
        
        // Act
        var result = await Assert.ThrowsAsync<CustomException>(() => _userFixture.UserService.CreateUser(invalidUser));
        var messageError = _userFixture.GetMessageResource("USER-CPF_EMPTY");
        
        // Assert
        result.Message.Should().Be(messageError);
    }
    
    [Fact(DisplayName = "Do not create if user is duplicated")]
    [Trait("Category", "User Service")]
    public async void User_NotCreate_IfUserDuplicated()
    {
        //Arrange
        _userFixture.GenerateUserService();
        var createdUser = _userFixture.CreateValidUser();
        var user = _userFixture.Mapper.Map<User>(createdUser);
        
        _userFixture.UserRepository.Setup(s =>
            s.Get(It.IsAny<Expression<Func<User, bool>>>())
        ).ReturnsAsync(user);
        
        //Act
        var result = await Assert.ThrowsAsync<CustomException>(() => _userFixture.UserService.CreateUser(createdUser));
        var messageError = _userFixture.GetMessageResource("USER-USER_EXISTING_BY_CPF_OR_EMAIL", user.Cpf, user.Email);
        
        //Assert
        result.Message.Should().Be(messageError);
    }
    
    [Fact(DisplayName = "Do not create if user phones duplicated")]
    [Trait("Category", "User Service")]
    public async void User_NotCreate_IfUserPhonesDuplicated()
    {
        // Arrange
        _userFixture.GenerateUserService();
        var createdUser = _userFixture.CreateValidUser();
        var user = _userFixture.Mapper.Map<User>(createdUser);
        var phonesExistings = user.UserPhones.Select(up => new string($"({up.Ddd}) {up.NumberPhone}")).ToList();
        
        _userFixture.UserRepository.Setup(s =>
            s.Get(It.IsAny<Expression<Func<User, bool>>>())
        ).ReturnsAsync((User)null);
        
        _userFixture.UserRepository.Setup(s =>
            s.ExistingPhone(It.IsAny<string>(), It.IsAny<string>())
        ).ReturnsAsync(true);
        
        // Act
        var result = await Assert.ThrowsAsync<CustomException>(() => _userFixture.UserService.CreateUser(createdUser));
        var messageError = _userFixture.GetMessageResource("USER-USERPHONE_EXISTINGS", string.Join(", ", phonesExistings));
        
        //Assert
        result.Message.Should().Be(messageError);
    }
    
    [Fact(DisplayName = "Do not create if user address is invalid")]
    [Trait("Category", "User Service")]
    public async void User_NotCreate_IfUserAddressInvalid()
    {
        // Arrange
        _userFixture.GenerateUserService();
        var createdUser = _userFixture.CreateValidUser();
        var user = _userFixture.Mapper.Map<User>(createdUser);

        _userFixture.UserRepository.Setup(s =>
            s.Get(It.IsAny<Expression<Func<User, bool>>>())
        ).ReturnsAsync((User)null);
        
        _userFixture.UserRepository.Setup(s =>
            s.ExistingPhone(It.IsAny<string>(), It.IsAny<string>())
        ).ReturnsAsync(false);
        
        _userFixture.ViaCepService.Setup(s =>
            s.GetAddressByPostalCode(It.IsAny<string>())
        ).ReturnsAsync((ViaCepResponseModel)null);
        
        // Act
        var result = await Assert.ThrowsAsync<CustomException>(() => _userFixture.UserService.CreateUser(createdUser));
        var messageError = _userFixture.GetMessageResource("USER-ADDRESS-NOT_FOUND_ADDRESS_BY_POSTAL_CODE", user.Address.PostalCode);
        
        //Assert
        result.Message.Should().Be(messageError);
    }
    
    [Fact(DisplayName = "Create user")]
    [Trait("Category", "User Service")]
    public async void User_Create_WithSuccess()
    {
        // Arrange
        _userFixture.GenerateUserService();
        var createdUser = _userFixture.CreateValidUser();
        var user = _userFixture.Mapper.Map<User>(createdUser);
        var viaCepResponse = _userFixture.CreateViaCepResponseModel();
        
        _userFixture.UserRepository.Setup(s =>
            s.Get(It.IsAny<Expression<Func<User, bool>>>())
        ).ReturnsAsync((User)null);
        
        _userFixture.UserRepository.Setup(s =>
            s.ExistingPhone(It.IsAny<string>(), It.IsAny<string>())
        ).ReturnsAsync(false);
        
        _userFixture.ViaCepService.Setup(s =>
            s.GetAddressByPostalCode(It.IsAny<string>())
        ).ReturnsAsync(viaCepResponse);
        
        _userFixture.UserRepository.Setup(s =>
            s.SaveChanges()
        );
        
        // Act
        var result = await _userFixture.UserService.CreateUser(createdUser);

        //Assert
        result.Should().NotBeNull();
    }
}