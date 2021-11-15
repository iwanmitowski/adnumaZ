using adnumaZ.Models;
using System.Threading.Tasks;

namespace adnumaZ.Services.UserService.Contracts
{
    public interface IUserService
    {
        public Task ChangeBanCondition(string userId, string banReason);
    }
}
