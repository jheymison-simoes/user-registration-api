using Microsoft.EntityFrameworkCore;
using UserRegister.Business.EntityModels;
using UserRegister.Business.Interfaces.Repositories;

namespace UserRegister.Data.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(SqlContext db) : base(db)
    {
    }

    public async Task<User> GetById(Guid id, bool withInclude)
    {
        var query = _dbSet.AsQueryable().AsNoTracking();
        if (withInclude)
            query = query
                .Include(u => u.Address)
                .Include(u => u.UserPhones);

        return await query.FirstOrDefaultAsync(u => u.Id == id);
    }
    
    #region User Phones
    public async Task<bool> ExistingPhone(string ddd, string numberPhone)
    {
        return await _db.UserPhones.AsNoTracking().AnyAsync(up => up.Ddd == ddd && up.NumberPhone == numberPhone);
    }
    #endregion
}