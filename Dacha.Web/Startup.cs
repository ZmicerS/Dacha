using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Dacha.Web.Startup))]

namespace Dacha.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {          
            ConfigureAuth(app);
        }
    }
}


