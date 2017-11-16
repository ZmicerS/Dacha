using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dacha.Dal.EF;
using Dacha.Dal.Entities;
using Dacha.Dal.Repositories;
using Dacha.Bll.Models;

namespace Dacha.Bll.Services
{
    public class UserService
    {
        // private ApplicationContext context = new ApplicationContext();

        private UnitOfWork _database;
     
        public  UserService()
        {
           _database = new UnitOfWork();
        }

        public UserService(string uow)
        {
           // Database = uow;
            //Configure AutoMapper 
            //AutoMapperConfiguration.Configure();
        }
        public void WriteCompanionship()
        {
            var companionship = new Companionship();
           companionship.Name = "Nazva";
            _database.CompanionshipRepository.Insert(companionship);
            _database.Save();
          //  context.Companionships.a
        }

        public IEnumerable<CompanionshipDto> GetOnlyAllCompanionship()
        {
            IEnumerable<CompanionshipDto> list = new List<CompanionshipDto>();
            IEnumerable<Companionship> listCompanionshipRepository;
            try
            {
                //throw new Exception("!!!!!!Ooops");
                Task<IEnumerable<Companionship>> task = Task.Run<IEnumerable<Companionship>>(() => {
                 //    throw new Exception("Ooops");
                     return _database.CompanionshipRepository.GetAllWithoutTracking().AsEnumerable().ToList();
             });

       task.Wait(360000);//milisecond
                if (task.IsCompleted)
                {
                    listCompanionshipRepository = task.Result;
                    if (listCompanionshipRepository != null)
                    {
                        list = listCompanionshipRepository.Select(s => new CompanionshipDto()
                        {
                            Id = s.Id,
                            Name = s.Name,
                            Address = s.Address,
                            Membership = s.Membership,
                            Registration = s.Registration,
                            Chairman = s.Chairman,
                            Addition = s.Addition
                        }).ToList();
                    }
                }

            }
           catch (AggregateException ae)
            {
                // throw ae.Flatten();           
            }

               
            return list;
        }


        public string Proba()
        {
           // throw new Exception("!!!!!!Ooops");
            try
            {
                Task task = Task.Run(()=> {
                    throw new Exception("Ooops");
                    Task.Delay(2000);
                    return;
                });
                task.Wait(360000);//milisecond
                return "try";
            }
            catch (AggregateException ae)
            {
                return "catch";
            }
            return "konec";
        }

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    //   userManager.Dispose();
                    //   roleManager.Dispose();
                    //      clientManager.Dispose();
                    _database.Dispose();
                }
                this.disposed = true;
            }
        }

#endregion 

    }
}
