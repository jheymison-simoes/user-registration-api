using System.Globalization;
using System.Resources;
using FluentValidation;
using UserRegister.Business.Exceptions;
using UserRegister.Business.Utils;

namespace UserRegister.Business.Models;

public class CreateUserAddressModel
{
    public string Street { get; set; }
    public string Number { get; set; }
    public string PostalCode { get; set; }
    public string District { get; set; }
    public string City { get; set; }
    public string State { get; set; }
}

public class CreateUserAddressValidator : AbstractValidator<CreateUserAddressModel>
{
    public CreateUserAddressValidator(ResourceManager resourceManager, CultureInfo cultureInfo)
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
    
    public async Task Validate(CreateUserAddressModel address, ResourceManager resourceManager, CultureInfo cultureInfo)
    {
        var validador = new CreateUserAddressValidator(resourceManager, cultureInfo);
        var result = await validador.ValidateAsync(address);
        if (result.IsValid) return;
        throw new CustomException(string.Join(" ", result.Errors));
    }
}