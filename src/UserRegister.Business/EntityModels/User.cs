using System.Globalization;
using System.Resources;
using FluentValidation;
using FluentValidation.Results;
using UserRegister.Business.Exceptions;
using UserRegister.Business.Models;
using UserRegister.Business.Utils;

namespace UserRegister.Business.EntityModels;

public class User : Entity
{
    public string Name { get; set; }
    public bool LegalPerson { get; set; }
    public string Cpf { get; set; }
    public string Cnpj { get; set; }
    public string CorporateName { get; set; }
    public Guid AddressId { get; set; }

    #region RelacionShip
    public Address Address { get; set; }
    public List<UserPhone> UserPhones { get; set; }
    #endregion
}

public class UserValidador : AbstractValidator<User>
{
    public UserValidador(ResourceManager resourceManager, CultureInfo cultureInfo)
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
            .MaximumLength(11)
            .WithMessage(resourceSet.GetResourceFormat("USER-CPF_EXCEEDED_MAXIMUM_CHARACTER", 11));

        When(u => u.LegalPerson, () =>
        {
            RuleFor(u => u.Cnpj)
                .NotEmpty()
                .WithMessage(resourceSet.GetResourceFormat("USER-CNPJ_EMPTY"))
                .MaximumLength(14)
                .WithMessage(resourceSet.GetResourceFormat("USER-CNPJ_EXCEEDED_MAXIMUM_CHARACTER", 14));

            RuleFor(u => u.CorporateName)
                .NotEmpty()
                .WithMessage(resourceSet.GetResourceFormat("USER-CORPORATENAME_EMPTY"))
                .MaximumLength(100)
                .WithMessage(resourceSet.GetResourceFormat("USER-CORPORATENAME_EXCEEDED_MAXIMUM_CHARACTER", 100));
        });
        
        RuleFor(u => u.Address)
            .SetValidator(new AddressValidator(resourceManager, cultureInfo));
    }

    public async Task Validate(User user, ResourceManager resourceManager, CultureInfo cultureInfo)
    {
        var validador = new UserValidador(resourceManager, cultureInfo);
        var result = await validador.ValidateAsync(user);
        if (result.IsValid) return;
        throw new CustomException(string.Join(" ", result.Errors));
    }
}