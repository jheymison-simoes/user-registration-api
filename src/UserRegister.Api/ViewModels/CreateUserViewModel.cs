namespace UserRegister.Api.ViewModels;

public class CreateUserViewModel
{
    public string Name { get; set; }
    public bool LegalPerson { get; set; } = false;
    public string Cpf { get; set; }
    public string Cnpj { get; set; }
    public string CorporateName { get; set; }
    public string Email { get; set; }
    public CreateUserAddressViewModel Address { get; set; }
    public List<CreateUserPhoneViewModel> UserPhones { get; set; }
}

public class CreateUserAddressViewModel
{
    public string Street { get; set; }
    public string Number { get; set; }
    public string PostalCode { get; set; }
    public string District { get; set; }
    public string City { get; set; }
    public string State { get; set; }
}

public class CreateUserPhoneViewModel
{
    public string Ddd { get; set; }
    public string NumberPhone { get; set; }
}