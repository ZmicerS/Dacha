using Dacha.Bll.Interfaces;
using Dacha.Bll.Models;
using Dacha.Dal.Entities;
using Dacha.Dal.Interfaces;
using Dacha.Dal.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dacha.Bll.Services
{
   public class CompanionshipService : ICompanionshipService
    {
        private IUnitOfWork _database;
    
        public CompanionshipService(string connection)
        {
          _database = new UnitOfWork(connection);
        }
     
        public IEnumerable<CompanionshipDto> GetOnlyAllCompanionship()
        {
            IEnumerable<CompanionshipDto> list = new List<CompanionshipDto>();
            IEnumerable<Companionship> listCompanionshipRepository;
            Task<IEnumerable<Companionship>> task = Task.Run<IEnumerable<Companionship>>(() => {            
                return _database.CompanionshipRepository.GetAllWithoutTracking().AsEnumerable().ToList();
            });
            try {
                task.Wait(36000);
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
                        }).ToList().OrderBy(o=>o.Name);
                    }
                }
            }
            catch (AggregateException ae)
            {                        
            }
            return list;
        }

         public async Task WriteCompanionshipAync(CompanionshipDto data)       
        {
            var companionship = new Companionship() {
                Name = data.Name,
                Address = data.Address,
                Registration = data.Registration,
                Chairman = data.Chairman,
                Membership = data.Membership,
                Addition = data.Addition
            };
            try
            {
                _database.CompanionshipRepository.Insert(companionship);
                 await _database.SaveAsync();           
            }
            catch (AggregateException ae)
            {

            }
        }

        public async Task UpdateCompanionshipAync(CompanionshipDto data)
        {
            Companionship companionship;          
            Task<Companionship> taskFind = Task<Companionship>.Run(() => _database.CompanionshipRepository.GetById(data.Id));
            try
            {
                taskFind.Wait(360000);
                if (taskFind.IsCompleted)
                {
                    companionship = taskFind.Result;
                    if (companionship!=null)
                    {
                        companionship.Name = data.Name;
                        companionship.Address = data.Address;
                        companionship.Registration = data.Registration;
                        companionship.Chairman = data.Chairman;
                        companionship.Membership = data.Membership;
                        companionship.Addition = data.Addition;
                        _database.CompanionshipRepository.Update(companionship);
                        await _database.SaveAsync();
                    }
                }
            }
            catch (AggregateException ae)
            {

            }

       }

        public async Task DeleteCompanionshipAync(string id)
        {
            try
            {
                Guid.TryParse(id, out Guid guid);
                _database.CompanionshipRepository.Delete(guid);
                await _database.SaveAsync();
            }
            catch(Exception e)
            {

            }
        }
    }
}
