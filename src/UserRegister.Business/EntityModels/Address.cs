using System.Globalization;
using System.Resources;
using FluentValidation;
using UserRegister.Business.Exceptions;
using UserRegister.Business.Models;
using UserRegister.Business.Utils;

namespace UserRegister.Business.EntityModels;

public class Address : Entity
{
    public string Street { get; set; }
    public string Number { get; set; }
    public string PostalCode { get; set; }
    public string District { get; set; }
    public string City { get; set; }
    public string State { get; set; }

    #region RelacionShip
    public User User { get; set; }
    #endregion
}

public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator(ResourceManager resourceManager, CultureInfo cultureInfo)
    {
        var resourceSet = resourceManager.GetResourceSet(cultureInfo, true, true);

        RuleFor(u => u.Street)
            .NotEmpty()
            .WithMessage(resourceSet.GetResourceFormat("ADDRESS-STREET_EMPTY"));
        
        RuleFor(u => u.Number)
            .NotEmpty()
            .WithMessage(resourceSet.GetResourceFormat("ADDRESS-NUMBER_EMPTY"));
        
        RuleFor(u => u.PostalCode)
            .NotEmpty()
            .WithMessage(resourceSet.GetResourceFormat("ADDRESS-POSTALCODE_EMPTY"))
            .Length(8)
            .WithMessage(resourceSet.GetResourceFormat("ADDRESS-POSTALCODE_INVALID"));
        
        RuleFor(u => u.District)
            .NotEmpty()
            .WithMessage(resourceSet.GetResourceFormat("ADDRESS-DISTRICT_EMPTY"));
        
        RuleFor(u => u.City)
            .NotEmpty()
            .WithMessage(resourceSet.GetResourceFormat("ADDRESS-CITY_EMPTY"));
        
        RuleFor(u => u.State)
            .NotEmpty()
            .WithMessage(resourceSet.GetResourceFormat("ADDRESS-STATE_EMPTY"));
    }
    
    public async Task Validate(Address address, ResourceManager resourceManager, CultureInfo cultureInfo)
    {
        var validador = new AddressValidator(resourceManager, cultureInfo);
        var result = await validador.ValidateAsync(address);
        if (result.IsValid) return;
        throw new CustomException(string.Join(" ", result.Errors));
    }
}