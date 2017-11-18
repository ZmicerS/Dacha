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
    public class MemberService : IMemberService
    {
        private IUnitOfWork _database;

        public MemberService(string connection)
        {
            _database = new UnitOfWork(connection);
        }
        
        public IEnumerable<MemberDto> GetMembersCompanionship(string companionshipId)
        {
            Guid guid;
            var membersList = new List<MemberDto>();
            if (Guid.TryParse(companionshipId, out guid))
            {              
                Task<IEnumerable<Member>> task = Task.Run<IEnumerable<Member>>(()=> {
                return _database.MemberRepository.Get(x => x.CompanionshipId == guid).ToList();
                });
                try
                {
                    task.Wait(360000);
                    if (task.IsCompleted)
                    {
                       var list = task.Result;
                        if (list!=null)
                        {
                            membersList = list.Select(s => new MemberDto()
                            {
                                Id = s.Id,
                                Owner = s.Owner,
                                OwnerAddress = s.OwnerAddress,
                                PlotNumber = s.PlotNumber,
                                PlotAddress = s.PlotAddress,
                                PlotSquare = s.PlotSquare,
                                Addition = s.Addition,
                                CompanionshipId = s.CompanionshipId
                            }).ToList();
                        }
                    }
                }
                catch(Exception e)
                {
                }             
            }
            return membersList;
        }

        public async Task WriteMemberAsync(MemberDto data)
        {
            Member member = new Member()
            {
                Owner = data.Owner,
                PlotNumber = data.PlotNumber,
                PlotAddress = data.PlotAddress,
                PlotSquare = data.PlotSquare,
                OwnerAddress = data.OwnerAddress,
                Addition = data.Addition,
                CompanionshipId = data.CompanionshipId
            };
            try
            { 
             _database.MemberRepository.Insert(member);
             await _database.SaveAsync();
            }
             catch (AggregateException ae)
             {
             }
        }

        public async Task UpdateMemberAsync(MemberDto data)
        {
            Member member;            
            Task<Member> taskFind = Task<Member>.Run(() => _database.MemberRepository.GetById(data.Id));
            try
            {
                taskFind.Wait(360000);
                if (taskFind.IsCompleted)
                {
                    member = taskFind.Result;
                    if(member!=null)
                    {
                     member.Id = data.Id;
                     member.Owner = data.Owner;
                     member.PlotNumber = data.PlotNumber;
                     member.PlotAddress = data.PlotAddress;
                     member.PlotSquare = data.PlotSquare;
                     member.OwnerAddress = data.OwnerAddress;
                     member.Addition = data.Addition;
                     member.CompanionshipId = data.CompanionshipId;
                        _database.MemberRepository.Update(member);
                        await _database.SaveAsync();
                    }
                }
            }
            catch (AggregateException ae)
            {
            }
        }

        public async Task DeleteMemberAsync(string id)
        {
            try
            {
                Guid.TryParse(id, out Guid guid);
                _database.MemberRepository.Delete(guid);
                await _database.SaveAsync();
            }
            catch (Exception e)
            {
            }
        }
    }
}
