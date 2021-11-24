using adnumaZ.Data;
using adnumaZ.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adnumaZ.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public UsersController(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<IActionResult> ById(string id)
        {
            var userFromDb = await dbContext.UserAccounts
                .Include(x => x.UploadedTorrents)
                .Include(x => x.DownloadedTorrents)
                .Include(x => x.FavouriteTorrents)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (userFromDb == null)
            {
                return NotFound();
            }

            var user = mapper.Map<UserViewModel>(userFromDb);

            return View(user);
        }
    }
}
