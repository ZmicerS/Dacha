using Dacha.Bll.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dacha.Bll.Interfaces
{
    public interface IMemberService
    {
        IEnumerable<MemberDto> GetMembersCompanionship(string companionshipId);
        Task WriteMemberAsync(MemberDto data);
        Task UpdateMemberAsync(MemberDto data);
        Task DeleteMemberAsync(string id);
    }
}
