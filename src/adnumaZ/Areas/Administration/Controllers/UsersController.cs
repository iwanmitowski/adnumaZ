using adnumaZ.Common.Constants;
using adnumaZ.Controllers;
using adnumaZ.Data;
using adnumaZ.Models;
using adnumaZ.Services.UserService.Contracts;
using adnumaZ.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace adnumaZ.Areas.Administration.Controllers
{
    [Authorize(Roles = Constants.AdministratorRoleName)]
    [Area("Administration")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;

        public UsersController(ApplicationDbContext dbContext, IMapper mapper, UserManager<User> userManager, IUserService userService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.userManager = userManager;
            this.userService = userService;
        }

        public IActionResult Index()
        {
            //OrderByDesc(x => x.ModifiedOn)

            var users = mapper
                .Map<List<UserViewModel>>(dbContext.Users)
                .OrderByDescending(x => x.ModifiedOn)
                .ThenByDescending(x => x.CreatedOn);

            return View(users);
        }

        public async Task<IActionResult> ById(string id)
        {
            var user = mapper.Map<UserViewModel>(await userManager.FindByIdAsync(id));

            if (user == null)
            {
                return RedirectToAction(nameof(this.Index));
            }

            return View(user);
        }

        public IActionResult Ban(string id)
        {
            var input = new BanInputModel()
            {
                UserId = id,
                BanReason = string.Empty,
            };

            return View(input);
        }

        [HttpPost]
        public async Task<IActionResult> Ban(BanInputModel input)
        {
            await userService.ChangeBanCondition(input.UserId, input.BanReason);

            return RedirectToAction(nameof(HomeController.Privacy));
        }

        public IActionResult Banned()
        {
            var users = mapper
               .Map<List<UserViewModel>>(dbContext.Users)
               .Where(x => x.IsBanned)
               .OrderByDescending(x => x.ModifiedOn)
               .ThenByDescending(x => x.CreatedOn);
            //return all banned users as the index view
            return View();
        }

        public IActionResult MakeAdmin(string id)
        {
            return RedirectToAction(nameof(this.Index));
        }

        public IActionResult MakeNormalUser(string id)
        {
            return RedirectToAction(nameof(this.Index));
        }
    }
}
