using UserRegister.Business.EntityModels;
using UserRegister.Business.Interfaces.Repositories;

namespace UserRegister.Data.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(SqlContext db) : base(db)
    {
    }
}