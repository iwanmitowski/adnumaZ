using adnumaZ.Common.Constants;
using adnumaZ.Models;
using adnumaZ.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace adnumaZ.Services.Mapping
{
    public class SetIsAdminMappingAction : IMappingAction<User, UserViewModel>
    {
        private readonly UserManager<User> userManager;

        public SetIsAdminMappingAction(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public void Process(User source, UserViewModel destination, ResolutionContext context)
        {
            var userRoles = userManager.GetRolesAsync(source).GetAwaiter().GetResult();

            if (userRoles.Select(x => x).Any(x => x.Contains(Constants.AdministratorRoleName)))
            {
                destination.IsAdmin = true;
            }
        }
    }
}
