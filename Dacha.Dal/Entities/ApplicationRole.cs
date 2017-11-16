using Microsoft.AspNet.Identity.EntityFramework;


namespace Dacha.Dal.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base()
        {
        }

        public ApplicationRole(string name)  : base(name)
        {
        }
    }
}
