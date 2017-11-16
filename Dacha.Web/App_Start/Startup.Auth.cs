using System;
using Microsoft.Owin;
using Owin;
using System.Configuration;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Dacha.Bll.Providers;
using System.Web.Http;
//
using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using Dacha.Bll.Configs;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Cors;

namespace Dacha.Web
{


    public partial class Startup
    {
        private static string _connectionStringName => ConfigurationManager.AppSettings["ConnectionStringName"] ?? "DachaContext";

        public void ConfigureAuth2(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
          
            var builder = new ContainerBuilder();
            // Register API controllers using assembly scanning.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // builder.Register(c => new AdvancedEncryptionStandardProvider(ConfigurationManager.AppSettings["rm:key"], ConfigurationManager.AppSettings["rm:secret"])).As<IAdvancedEncryptionStandardProvider>();
            AutofacConfig.Create(_connectionStringName, ref builder);
            // Build
            var container = builder.Build();

            // Lets Web API know it should locate services using the AutofacWebApiDependencyResolver
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
           
            var scope = config.DependencyResolver.GetRootLifetimeScope();

            // Get our providers
            var refreshTokenProvider = scope.Resolve<IAuthenticationTokenProvider>();
            var authProvider = scope.Resolve<OAuthAuthorizationServerProvider>();
           

            var issuer = ConfigurationManager.AppSettings["issuer"];
            var secretkey = ConfigurationManager.AppSettings["secret"];
            var seccod = TextEncodings.Base64Url.Decode(secretkey);
            var secret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["secret"]);

            app.UseCors(CorsOptions.AllowAll);

            // Register the Autofac middleware FIRST. This also adds
            // Autofac-injected middleware registered with the container.
            app.UseAutofacMiddleware(container);

            //
            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                AllowedAudiences = new[] { "Any" },
                IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
              {
                    new SymmetricKeyIssuerSecurityTokenProvider(issuer, secret)
              }
            });

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
#if DEBUG
                AllowInsecureHttp = true,
#endif
                TokenEndpointPath = new PathString("/oauth2/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(60),
                //  Provider = new CustomOAuthProvider(),
                Provider = authProvider,
                AccessTokenFormat = new CustomJwtFormat(issuer),
                RefreshTokenProvider = refreshTokenProvider
            });
            app.UseWebApi(config);

        }



        public void ConfigureAuth(IAppBuilder app)
        {           
            HttpConfiguration httpConfiguration = new HttpConfiguration();
            WebApiConfig.Register(httpConfiguration);
          
            var builder = new ContainerBuilder();
            // Register API controllers using assembly scanning.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            AutofacConfig.Create(_connectionStringName,ref builder);
            
            //Set the dependency resolver to be Autofac.  
            var container = builder.Build();      
            var resolver = new AutofacWebApiDependencyResolver(container);
            httpConfiguration.DependencyResolver = resolver;

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(httpConfiguration);
            
            var issuer = ConfigurationManager.AppSettings["issuer"];
            var secretkey = ConfigurationManager.AppSettings["secret"];
            var seccod = TextEncodings.Base64Url.Decode(secretkey);
            var secret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["secret"]);

            //  app.CreatePerOwinContext(ApplicationDbContext.Create);
            //  app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                AllowedAudiences = new[] { "Any" },
                IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
              {
                    new SymmetricKeyIssuerSecurityTokenProvider(issuer, secret)
              }
            });

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
#if DEBUG
                AllowInsecureHttp = true,
#endif
                TokenEndpointPath = new PathString("/oauth2/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(60),               
                Provider = new CustomOAuthProvider(_connectionStringName),
                AccessTokenFormat = new CustomJwtFormat(issuer),
                RefreshTokenProvider = new RefreshTokenProvider(_connectionStringName)
            });

            //must be here    
            app.UseWebApi(httpConfiguration);
            //
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

        


        }
    }



}

