using Dacha.Dal.Entities;
using System.Data.Entity;

namespace Dacha.Dal.Repositories
{
    public class RefreshTokenRepository : Repository<RefreshToken>
    {
       public RefreshTokenRepository(DbContext dbContext) : base(dbContext)
       {

       }
    }
}
