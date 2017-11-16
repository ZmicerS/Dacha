using Dacha.Dal.Entities;
using Dacha.Dal.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dacha.Dal.Interfaces
{
    public  interface IUnitOfWork : IDisposable
    {        
       ApplicationUserManager UserManager { get; }
      
       ApplicationRoleManager RoleManager { get; }
       IRepository<RefreshToken> RefreshTokenRepository { get; }
        //
       IRepository<Companionship> CompanionshipRepository { get; }
       IRepository<Member> MemberRepository { get; }
       IRepository<MemberDoc> MemberDocRepository { get; }

       /// <summary>
       /// Saves the changes Dbcontext.
       /// </summary>
       /// <returns></returns>
       void Save();

        /// <summary>
        /// Saves the changes async Dbcontext.
        /// </summary>
        /// <returns>Task</returns>
        Task SaveAsync();
    }

}
