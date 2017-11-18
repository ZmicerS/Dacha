using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Dacha.Dal.Repositories;
using Dacha.Dal.Interfaces;

namespace Dacha.Bll.Providers
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        private IUnitOfWork _database;

        public CustomOAuthProvider(string connectionString)
        {
            _database = new UnitOfWork(connectionString);
        }

        /// <summary>
        ///     Called when a request to the Token endpoint arrives with a "grant_type" of "password". This occurs when the user
        ///     has provided name and password credentials directly into the client application's user interface, and the client
        ///     application is using those to acquire an "access_token" and optional "refresh_token". If the web application
        ///     supports the resource owner credentials grant type it must validate the context.Username and context.Password as
        ///     appropriate. To issue an access token the context.Validated must be called with a new ticket containing the claims
        ///     about the resource owner which should be associated with the access token. The application should take appropriate
        ///     measures to ensure that the endpoint isn’t abused by malicious callers.
        ///     The default behavior is to reject this grant type.
        ///     See also http://tools.ietf.org/html/rfc6749#section-4.3.2
        /// </summary>
        /// <param name="context">The context of the event carries information in and results out.</param>
        /// <returns>Task to enable asynchronous execution</returns>
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
         
            var user=_database.UserManager.Users.FirstOrDefault(u => u.UserName == context.UserName);
            var checkPassword = _database.UserManager.CheckPassword(user, context.Password);
          
            if (!checkPassword)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect");
                context.Rejected();
                return Task.FromResult<object>(null);
            }

            var ticket = new AuthenticationTicket(SetClaimsIdentity(context, user), new AuthenticationProperties());
            context.Validated(ticket);

            return Task.FromResult<object>(null);
        }

        /// <summary>
        ///     Called to validate that the origin of the request is a registered "client_id", and that the correct credentials for
        ///     that client are present on the request. If the web application accepts Basic authentication credentials,
        ///     context.TryGetBasicCredentials(out clientId, out clientSecret) may be called to acquire those values if present in
        ///     the request header. If the web application accepts "client_id" and "client_secret" as form encoded POST parameters,
        ///     context.TryGetFormCredentials(out clientId, out clientSecret) may be called to acquire those values if present in
        ///     the request body.
        ///     If context.Validated is not called the request will not proceed further.
        /// </summary>
        /// <param name="context">The context of the event carries information in and results out.</param>
        /// <returns>Task to enable asynchronous execution</returns>
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        private  ClaimsIdentity SetClaimsIdentity(OAuthGrantResourceOwnerCredentialsContext context, IdentityUser user)
        {
            var identity = new ClaimsIdentity("JWT");
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim("sub", context.UserName));

            var userRoles = _database.UserManager.GetRoles(user.Id);
            foreach (var role in userRoles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }
            return identity;
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            // Change authentication ticket for refresh token requests  
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
            newIdentity.AddClaim(new Claim("newClaim", "newValue"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }
    }
}
