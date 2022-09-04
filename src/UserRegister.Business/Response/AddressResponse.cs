namespace UserRegister.Business.Response;

public class AddressResponse
{
    public Guid Id { get; set; }
    public int Code { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string PostalCode { get; set; }
    public string District { get; set; }
    public string City { get; set; }
    public string State { get; set; }
}