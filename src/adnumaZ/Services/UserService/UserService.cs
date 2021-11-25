using adnumaZ.Common.Constants;
using adnumaZ.Data;
using adnumaZ.Models;
using adnumaZ.Services.UserService.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace adnumaZ.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<User> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserService(ApplicationDbContext dbContext ,UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<User> GetUser(string userId)
        {
            return await userManager.FindByIdAsync(userId);
        }

        public async Task<User> GetCurrentUserAsync()
        {
            return await userManager.GetUserAsync(httpContextAccessor.HttpContext.User);
        }

        public async Task ChangeBanCondition(string userId, [Optional] string banReason)
        {
            var user = await GetUser(userId);

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

        public async Task PromoteToAdmin(string userId)
        {
            var user = await GetUser(userId);

            await userManager.AddToRoleAsync(user, Constants.AdministratorRoleName);
            user.ModifiedOn = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();
        }

        public async Task DemoteToUser(string userId)
        {
            var user = await GetUser(userId);

            await userManager.RemoveFromRoleAsync(user, Constants.AdministratorRoleName);
            user.ModifiedOn = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();
        }
    }
}
