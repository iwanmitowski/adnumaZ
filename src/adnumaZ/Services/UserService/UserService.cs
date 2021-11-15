using adnumaZ.Models;
using adnumaZ.Services.UserService.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adnumaZ.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> userManager;

        public UserService(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task ChangeBanCondition(string userId, string banReason)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return;
            }

            if (user.IsBanned)
            {
                user.BannedOn = null;
                user.BanReason = null;
            }
            else
            {
                user.BannedOn = DateTime.UtcNow;
                user.BanReason = banReason;
            }

            user.ModifiedOn = DateTime.UtcNow;
            user.IsBanned = !user.IsBanned;
        }
    }
}
