using UserRegister.Business.Models;
using UserRegister.Business.Response;

namespace UserRegister.Business.Interfaces.Services;

public interface IUserService
{
    Task<UserResponse> CreateUser(CreateUserModel createUser);
}