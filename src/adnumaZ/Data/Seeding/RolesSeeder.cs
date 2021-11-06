using adnumaZ.Data.Seeding.Contracts;
using adnumaZ.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace adnumaZ.Data.Seeding
{
    public class RolesSeeder : ISeeder
    {
        private const string AdministratorRoleName = "Administrator";
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            await SeedRoleAsync(roleManager);
        }

        private async Task SeedRoleAsync(RoleManager<ApplicationRole> roleManager)
        {
            var role = await roleManager.FindByNameAsync(AdministratorRoleName);

            if (role != null)
            {
                return;
            }

            var result = await roleManager.CreateAsync(new ApplicationRole(AdministratorRoleName));

            if (!result.Succeeded)
            {
                throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
            }
        }
    }
}
