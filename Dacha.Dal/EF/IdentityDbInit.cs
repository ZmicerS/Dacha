using Dacha.Dal.Entities;
using Dacha.Dal.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;


namespace Dacha.Dal.EF
{   
    public class IdentityDbInit : CreateDatabaseIfNotExists<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }
        public void PerformInitialSetup(ApplicationContext context)
        {
            // Context configuration settings will be specified here
            ApplicationUserManager userMgr = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            ApplicationRoleManager roleMgr = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));

            string roleNameA = "Administrator";
            string userName = "admin@mail.ru";
            string password = "Mypassword1234.";
            string email = "admin@mail.ru";
            string roleNameUs = "User";

            if (!roleMgr.RoleExists(roleNameA))
            {
                roleMgr.Create(new ApplicationRole(roleNameA));
            }

            if (!roleMgr.RoleExists(roleNameUs))
            {
                roleMgr.Create(new ApplicationRole(roleNameUs));
            }
            ApplicationUser user = userMgr.FindByName(userName);
            if (user == null)
            {
                userMgr.Create(new ApplicationUser { UserName = userName, Email = email },
                    password);
                user = userMgr.FindByName(userName);
            }

            if (!userMgr.IsInRole(user.Id, roleNameA))
            {
                userMgr.AddToRole(user.Id, roleNameA);
            }
        }


    }

}
