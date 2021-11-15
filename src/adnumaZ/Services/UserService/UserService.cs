using adnumaZ.Data;
using adnumaZ.Models;
using adnumaZ.Services.UserService.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adnumaZ.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<User> userManager;

        public UserService(ApplicationDbContext dbContext ,UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task ChangeBanCondition(string userId, string banReason)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new InvalidOperationException();
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

            await dbContext.SaveChangesAsync();
        }
    }
}
