using Microsoft.EntityFrameworkCore;
using UserRegister.Business.EntityModels;
using UserRegister.Business.Interfaces.Repositories;

namespace UserRegister.Data.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(SqlContext db) : base(db)
    {
    }

    #region User Phones
    public async Task<bool> ExistingPhone(string ddd, string numberPhone)
    {
        return await _db.UserPhones.AsNoTracking().AnyAsync(up => up.Ddd == ddd && up.NumberPhone == numberPhone);
    }
    #endregion
}