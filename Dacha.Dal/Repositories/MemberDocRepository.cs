using Dacha.Dal.Entities;
using System.Data.Entity;


namespace Dacha.Dal.Repositories
{
    public class MemberDocRepository : Repository<MemberDoc>
    {
        public MemberDocRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }

}
