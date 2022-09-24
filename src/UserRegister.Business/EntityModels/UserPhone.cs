using System.Globalization;
using System.Resources;
using FluentValidation;
using UserRegister.Business.Exceptions;
using UserRegister.Business.Models;
using UserRegister.Business.Utils;

namespace UserRegister.Business.EntityModels;

public class UserPhone : Entity
{
    public Guid UserId { get; set; }
    public string Ddd { get; set; }
    public string NumberPhone { get; set; }

    #region RelacionShip
    public User User { get; set; }
    #endregion
}

public class UserPhoneValidator : AbstractValidator<UserPhone>
{
    public UserPhoneValidator(ResourceManager resourceManager, CultureInfo cultureInfo)
    {
        var resourceSet = resourceManager.GetResourceSet(cultureInfo, true, true);
        
        RuleFor(up => up.Ddd)
            .NotEmpty()
            .WithMessage(resourceSet.GetResourceFormat("USER-PHONE-DDD_EMPTY"))
            .MaximumLength(2)
            .WithMessage(resourceSet.GetResourceFormat("USER-PHONE-DDD_EXCEEDED_MAXIMUM_CHARACTER", 2));
        
        RuleFor(up => up.Ddd)
            .NotEmpty()
            .WithMessage(resourceSet.GetResourceFormat("USER-PHONE-NUMBERPHONE_EMPTY"))
            .MaximumLength(9)
            .WithMessage(resourceSet.GetResourceFormat("USER-PHONE-NUMBERPHONE_EXCEEDED_MAXIMUM_CHARACTER", 11));
    }

    public async Task Validation(UserPhone userPhone)
    {
        var validator =  await ValidateAsync(userPhone);
        if (validator.IsValid) return;
        throw new CustomException(string.Join(" ", validator.Errors));
    }
}