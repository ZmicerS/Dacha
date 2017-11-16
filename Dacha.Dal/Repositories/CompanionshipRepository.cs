using Dacha.Dal.Entities;
using System.Data.Entity;

namespace Dacha.Dal.Repositories
{  
    public class CompanionshipRepository : Repository<Companionship>
    {
        public CompanionshipRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
