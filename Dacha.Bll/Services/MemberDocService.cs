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
   public  class MemberDocService : IMemberDocService
   {
        private IUnitOfWork _database;     

       public MemberDocService(string connection)
       {
            _database = new UnitOfWork(connection);
       }
        
        public void UploadSingleFile(MemberDocDto memberDocDto)
        {
            var memberDoc = new MemberDoc()
            {
              NameDoc=memberDocDto.NameDoc,
              Document=memberDocDto.Document,
              DocumentMimeType=memberDocDto.DocumentMimeType,
              Description= memberDocDto.Description,
              MemberId=memberDocDto.MemberId
            };
            _database.MemberDocRepository.Insert(memberDoc);
            _database.Save();
        }

        public IEnumerable<string> GetListById(string id)
        {
            Guid guid;
            var idList = new List<string>();
            Guid.TryParse(id, out guid);
            
            var list = _database.MemberDocRepository.Get(m => m.MemberId == guid).ToList();
            if (list != null)
            {
                idList = list.Select(s => s.Id.ToString()).ToList();
            }            
            return idList;
        }

        public MemberDocDto GeMemberDoc(string id)
        {
            Guid guid;
            Guid.TryParse(id, out guid);
            MemberDocDto memberDocDto = new MemberDocDto();
            var doc = _database.MemberDocRepository.Get(m => m.Id == guid).FirstOrDefault();
            if (doc!=null)
            {
                memberDocDto.Id = doc.Id;
                memberDocDto.NameDoc = doc.NameDoc;
                memberDocDto.Document = doc.Document;
                memberDocDto.DocumentMimeType = doc.DocumentMimeType;
            }
            return memberDocDto;
        }

        public IEnumerable<MemberDocDto> GetListDocMemberById(string id)
        {
            Guid guid;
            var memberDocsDtoList = new List<MemberDocDto>();
            Guid.TryParse(id, out guid);
            
            var list=  _database.MemberDocRepository.Get(m => m.MemberId == guid).ToList();
            if(list!=null)
            {
                memberDocsDtoList = list.Select(s => new MemberDocDto()
                {
                    Id = s.Id,
                    NameDoc = s.NameDoc,
                    Document = s.Document,
                    DocumentMimeType = s.DocumentMimeType,
                    Description = s.Description,
                    MemberId = s.MemberId ?? guid
                }).ToList();
            }
            return memberDocsDtoList;
        }

        public async Task DeleteMemberDocAync(string id)
        {
            try
            {
                if (Guid.TryParse(id, out Guid guid))
                {
                    _database.MemberDocRepository.Delete(guid);
                    await _database.SaveAsync();
                }
            }
            catch (Exception e)
            {
            }
        }
   }
}


