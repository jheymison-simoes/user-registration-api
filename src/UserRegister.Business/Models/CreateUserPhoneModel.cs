using System.Globalization;
using System.Resources;
using FluentValidation;
using UserRegister.Business.EntityModels;
using UserRegister.Business.Exceptions;
using UserRegister.Business.Utils;

namespace UserRegister.Business.Models;

public class CreateUserPhoneModel
{
    public string Ddd { get; set; }
    public string NumberPhone { get; set; }
}

public class CreateUserPhoneValidator : AbstractValidator<CreateUserPhoneModel>
{
    public CreateUserPhoneValidator(ResourceManager resourceManager, CultureInfo cultureInfo)
    {
        var resourceSet = resourceManager.GetResourceSet(cultureInfo, true, true);
    
        RuleFor(up => up.Ddd)
            .NotEmpty()
            .WithMessage(resourceSet.GetResourceFormat("USER-PHONE-DDD_EMPTY"))
            .MaximumLength(2)
            .WithMessage(resourceSet.GetResourceFormat("USER-PHONE-DDD_EXCEEDED_MAXIMUM_CHARACTER", 2));
    
        RuleFor(up => up.NumberPhone)
            .NotEmpty()
            .WithMessage(resourceSet.GetResourceFormat("USER-PHONE-NUMBERPHONE_EMPTY"))
            .MaximumLength(9)
            .WithMessage(resourceSet.GetResourceFormat("USER-PHONE-NUMBERPHONE_EXCEEDED_MAXIMUM_CHARACTER", 9));
    }

    public async Task Validate(CreateUserPhoneModel userPhone, ResourceManager resourceManager, CultureInfo cultureInfo)
    {
        var validador = new CreateUserPhoneValidator(resourceManager, cultureInfo);
        var result = await validador.ValidateAsync(userPhone);
        if (result.IsValid) return;
        throw new CustomException(string.Join(" ", result.Errors));
    }
}