namespace UserRegister.Business.Response;

public class UserPhoneResponse
{
    public Guid Id { get; set; }
    public int Code { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid UserId { get; set; }
    public string Ddd { get; set; }
    public string NumberPhone { get; set; }
}