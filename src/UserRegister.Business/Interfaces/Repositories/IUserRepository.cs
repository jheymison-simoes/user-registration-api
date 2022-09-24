using UserRegister.Business.EntityModels;

namespace UserRegister.Business.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User> GetById(Guid id, bool withInclude);
    Task<List<User>> GetAll(bool withInclude = false);
    
    #region User Phones
    Task<bool> ExistingPhone(string ddd, string numberPhone);
    #endregion
}