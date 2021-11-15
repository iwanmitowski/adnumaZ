using adnumaZ.Common.Constants;
using adnumaZ.Data;
using Microsoft.AspNetCore.Authorization;
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

        public UsersController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var users = dbContext.Users.ToList();
            return View(users);
        }

        public IActionResult Ban(string id)
        {
            return View();
        }

        public IActionResult Unban(string id)
        {
            return View();
        }

        public IActionResult MakeAdmin(string id)
        {
            return View();
        }

        public IActionResult MakeNormalUser(string id)
        {
            return View();
        }
    }
}
