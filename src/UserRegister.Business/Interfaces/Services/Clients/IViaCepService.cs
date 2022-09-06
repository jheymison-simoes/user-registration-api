using UserRegister.Business.Models.Clients;

namespace UserRegister.Business.Interfaces.Services.Clients;

public interface IViaCepService
{
    Task<ViaCepResponseModel> GetAddressByPostalCode(string cep);
}