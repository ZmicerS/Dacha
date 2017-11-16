using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using Dacha.Dal.Entities;
using Dacha.Dal.Repositories;
using Dacha.Dal.Interfaces;

namespace Dacha.Bll.Providers
{

    public class RefreshTokenProvider : IAuthenticationTokenProvider
    {
        private IUnitOfWork _database;
       
        public RefreshTokenProvider(string connectionString)
        {
           _database = new UnitOfWork(connectionString);
        }
        
        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var guid = Guid.NewGuid().ToString();

            // copy all properties and set the desired lifetime of refresh token  
            var refreshTokenProperties = new AuthenticationProperties(context.Ticket.Properties.Dictionary)
            {
                IssuedUtc = context.Ticket.Properties.IssuedUtc,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(60)
            };

            var refreshTokenTicket = new AuthenticationTicket(context.Ticket.Identity, refreshTokenProperties);
            //////
            var token = new RefreshToken()
            {
                Id = guid, //Helper.GetHash(refreshTokenId)
                ClientId = guid, //clientid
                Subject = context.Ticket.Identity.Name,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(60))
            };

            token.ProtectedTicket = context.SerializeTicket();
    
            var existingToken = _database.RefreshTokenRepository.Get(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();

            if (existingToken != null)
            {               
                _database.RefreshTokenRepository.Delete(existingToken);              
                _database.Save();
            }

            _database.RefreshTokenRepository.Insert(token);
            _database.Save();

           
            // consider storing only the hash of the handle  
            context.SetToken(guid);
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {            
            AuthenticationTicket ticket;
            string header = context.OwinContext.Request.Headers["Authorization"];           
          
            var refreshToken = _database.RefreshTokenRepository.GetById(context.Token);

            if (refreshToken != null)
            {   //Get protectedTicket from refreshToken class
                context.DeserializeTicket(refreshToken.ProtectedTicket);
                               
                _database.RefreshTokenRepository.Delete(refreshToken);
                
                _database.Save();
            }
           
        }
    }


}
