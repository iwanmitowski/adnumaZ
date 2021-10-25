using adnumaZ.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adnumaZ.Controllers
{
    public class TorrentController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public TorrentController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult ById(int id)
        {
            return View();
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(int todo)
        {
            return View();
        }

        public IActionResult All()
        {
            return View();
        }
    }
}
