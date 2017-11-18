using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace Dacha.Dal.Entities
{
    public class ApplicationUser : IdentityUser
    {       
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {            
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);          
            return userIdentity;
        }
    }
}
