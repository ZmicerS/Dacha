using Dacha.Bll.Interfaces;
using Dacha.Bll.Models;
using Dacha.Dal.Entities;
using Dacha.Dal.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;

namespace Dacha.Bll.Services
{
    public class AccountService : IAccountService
    {
        private UnitOfWork _database;
     
        public AccountService(string connection)
        {
            _database = new UnitOfWork(connection);

        }
        
        public AccountService(UnitOfWork unitOfWork)
        {
            _database = unitOfWork;

        }


        public async Task RegisterUserAsync(RegisterModelDto registerModel)
        {
            string roleNameUs = "User";         
            try
            {
                var userManager = _database.UserManager;
                var user = await userManager.StoreofUser.FindByNameAsync(registerModel.Email);
                
                if (user != null)
                {
                    throw new Exception("Such user exist alredy");
                }
                user = new ApplicationUser { UserName = registerModel.Email, Email = registerModel.Email };
                IdentityResult result = await userManager.CreateAsync(user, registerModel.Password);
                if (!result.Succeeded)
                {
                    throw new Exception(@"Can't crete user");
                }
                user = await userManager.FindByNameAsync(registerModel.Email);
                if (!userManager.IsInRole(user.Id, roleNameUs))
                {
                    await userManager.AddToRoleAsync(user.Id, roleNameUs);
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            return;
        }


    }

}

