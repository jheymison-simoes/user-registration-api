namespace UserRegister.Business.Response;

public class UserResponse
{
    public Guid Id { get; set; }
    public int Code { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Name { get; set; }
    public bool LegalPerson { get; set; }
    public string Cpf { get; set; }
    public string Cnpj { get; set; }
    public string CorporateName { get; set; }
    public Guid AddressId { get; set; }
    public AddressResponse Address { get; set; }
    public List<UserPhoneResponse> UserPhones { get; set; }
}