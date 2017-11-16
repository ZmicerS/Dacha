using Dacha.Dal.Entities;
using System.Data.Entity;

namespace Dacha.Dal.Repositories
{
    public class MemberRepository : Repository<Member>
    {
        public MemberRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }

}
