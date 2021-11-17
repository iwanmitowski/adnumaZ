using adnumaZ.Common.Constants;
using adnumaZ.Controllers;
using adnumaZ.Data;
using adnumaZ.Models;
using adnumaZ.Services.UserService.Contracts;
using adnumaZ.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly IHttpContextAccessor httpContextAccessor;

        public UsersController(ApplicationDbContext dbContext, IMapper mapper, UserManager<User> userManager, IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.userManager = userManager;
            this.userService = userService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            var users = mapper
                .Map<List<UserViewModel>>(dbContext.Users)
                .Where(x=>x.Id != user.Id)
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

        [HttpPost]
        public async Task<IActionResult> Unban([FromForm] string id)
        {
            await userService.ChangeBanCondition(id);

            return RedirectToAction(nameof(this.Index));
        }

        public IActionResult Banned()
        {
            var users = mapper
               .Map<List<UserViewModel>>(dbContext.Users)
               .Where(x => x.IsBanned)
               .OrderByDescending(x => x.ModifiedOn)
               .ThenByDescending(x => x.CreatedOn);

            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> PromoteToAdmin(string id)
        {
            await userService.PromoteToAdmin(id);

            return RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        public async  Task<IActionResult> DemoteToUser(string id)
        {
            await userService.DemoteToUser(id);

            return RedirectToAction(nameof(this.Index));
        }
    }
}
