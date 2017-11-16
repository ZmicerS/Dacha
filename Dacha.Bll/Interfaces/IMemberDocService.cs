using Dacha.Bll.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dacha.Bll.Interfaces
{
    public interface IMemberDocService
    {
        void UploadSingleFile(MemberDocDto memberDocDto);
        IEnumerable<string> GetListById(string id);
        MemberDocDto GeMemberDoc(string id);
        IEnumerable<MemberDocDto> GetListDocMemberById(string id);
        Task DeleteMemberDocAync(string id);
    }
}
