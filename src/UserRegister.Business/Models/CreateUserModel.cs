using System.Globalization;
using System.Resources;
using FluentValidation;
using UserRegister.Business.EntityModels;
using UserRegister.Business.Exceptions;
using UserRegister.Business.Utils;

namespace UserRegister.Business.Models;

public class CreateUserModel
{
    public string Name { get; set; }
    public bool LegalPerson { get; set; }
    public string Cpf { get; set; }
    public string Cnpj { get; set; }
    public string CorporateName { get; set; }
    public string Email { get; set; }
    public CreateUserAddressModel Address { get; set; }
    public List<CreateUserPhoneModel> UserPhones { get; set; }
}

public class CreateUserValidator : AbstractValidator<CreateUserModel>
{
    public CreateUserValidator(ResourceManager resourceManager, CultureInfo cultureInfo)
    {
        var resourceSet = resourceManager.GetResourceSet(cultureInfo, true, true);

        RuleFor(u => u.Name)
            .NotEmpty()
            .WithMessage(resourceSet.GetResourceFormat("USER-NAME_EMPTY"))
            .MaximumLength(100)
            .WithMessage(resourceSet.GetResourceFormat("USER-NAME_EXCEEDED_MAXIMUM_CHARACTER", 100));
        
        RuleFor(u => u.Cpf)
            .NotEmpty()
            .WithMessage(resourceSet.GetResourceFormat("USER-CPF_EMPTY"))
            .MinimumLength(11)
            .WithMessage(resourceSet.GetResourceFormat("USER-CPF_EXCEEDED_MAXIMUM_CHARACTER", 11));

        RuleFor(u => u.Email)
            .NotEmpty()
            .WithMessage(resourceSet.GetResourceFormat("USER-EMAIL_EMPTY"))
            .EmailAddress()
            .WithMessage(resourceSet.GetResourceFormat("USER-EMAIL_INVALID"));
        
        RuleFor(u => u.Cnpj)
            .NotEmpty()
            .Unless(x => !x.LegalPerson)
            .WithMessage(resourceSet.GetResourceFormat("USER-CNPJ_EMPTY"))
            .Length(14)
            .WithMessage(resourceSet.GetResourceFormat("USER-CNPJ_EXCEEDED_MAXIMUM_CHARACTER", 14));

        RuleFor(u => u.CorporateName)
            .NotEmpty()
            .Unless(x => !x.LegalPerson)
            .WithMessage(resourceSet.GetResourceFormat("USER-CORPORATENAME_EMPTY"))
            .MaximumLength(100)
            .WithMessage(resourceSet.GetResourceFormat("USER-CORPORATENAME_EXCEEDED_MAXIMUM_CHARACTER", 100));
        
        RuleFor(u => u.Address)
            .SetValidator(new CreateUserAddressValidator(resourceManager, cultureInfo));

        RuleForEach(u => u.UserPhones)
            .NotEmpty()
            .WithMessage(resourceSet.GetResourceFormat("USER-USERPHONES_EMPTY"))
            .SetValidator(new CreateUserPhoneValidator(resourceManager, cultureInfo));
    }
    
    public async Task Validate(CreateUserModel user, ResourceManager resourceManager, CultureInfo cultureInfo)
    {
        var validador = new CreateUserValidator(resourceManager, cultureInfo);
        var result = await validador.ValidateAsync(user);
        if (result.IsValid) return;
        throw new CustomException(string.Join(" ", result.Errors));
    }
}