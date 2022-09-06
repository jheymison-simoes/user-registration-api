using UserRegister.Business.EntityModels;

namespace UserRegister.Business.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    #region User Phones
    Task<bool> ExistingPhone(string ddd, string numberPhone);
    #endregion
}