using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adnumaZ.Areas.Administration.Controllers
{
    [Authorize(Roles = AdministratorRoleName)]
    [Area("Administration")]
    public class UsersController : Controller
    {
        private const string AdministratorRoleName = "Administrator";
        public IActionResult Index()
        {
            return View();
        }

        //Ban/Unban/Make Admin
    }
}
