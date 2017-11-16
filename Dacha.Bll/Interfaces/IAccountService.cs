using Dacha.Bll.Models;
using System.Threading.Tasks;

namespace Dacha.Bll.Interfaces
{
    public interface IAccountService
    {
        Task RegisterUserAsync(RegisterModelDto registerModel);
    }
}
