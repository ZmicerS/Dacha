using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Dacha.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
/*
 * https://www.developerhandbook.com/c-sharp/create-restful-api-authentication-using-web-api-jwt/
 * Dal
 * System.Configuration
 * EntityFramework
 * 
 * 
 * 
 * Web
 * EntityFramework
 * 
 * Microsoft.AspNet.Identity.Core
Microsoft.AspNet.Identity.Owin
Microsoft.Owin.Host.SystemWeb
Owin
 * 
 * js
 * AngularJS.Core
Angular.UI.UI-Router
Angular.UI.Bootstrap
bootstrap
jQuery


DAL
Microsoft.AspNet.Identity.Core and web and bll
Microsoft.AspNet.Identity.EntityFramework
Microsoft.AspNet.WebApi.Owin

    https://stackoverflow.com/questions/26371214/passing-applicationusermanager-to-applicationoauthprovider-with-autofac-with-asp/35428124
 */
