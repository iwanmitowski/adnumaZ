using adnumaZ.Models;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace adnumaZ.Services.UserService.Contracts
{
    public interface IUserService
    {
        public Task<User> GetUser(string userId);
        public Task ChangeBanCondition(string userId, [Optional] string reason);
        public Task PromoteToAdmin(string userId);
        public Task DemoteToUser(string userId);
    }
}
