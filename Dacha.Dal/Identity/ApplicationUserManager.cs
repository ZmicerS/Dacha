using Dacha.Dal.EF;
using Dacha.Dal.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dacha.Dal.Identity
{

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        private IUserStore<ApplicationUser> _store;

        /// <summary>
        ///    handle for access operation
        ///     wuth usermanager
        /// </summary>
        public IUserStore<ApplicationUser> StoreofUser => _store;
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store)
        {
            _store = store;
            UserValidator = new UserValidator<ApplicationUser>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,             
            };
            
        }
      
    }
}
