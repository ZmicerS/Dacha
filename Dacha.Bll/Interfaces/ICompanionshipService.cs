using Dacha.Bll.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dacha.Bll.Interfaces
{
    public interface ICompanionshipService
    {
        IEnumerable<CompanionshipDto> GetOnlyAllCompanionship();
        Task WriteCompanionshipAync(CompanionshipDto data);
        Task UpdateCompanionshipAync(CompanionshipDto data);
        Task DeleteCompanionshipAync(string id);
    }
}
