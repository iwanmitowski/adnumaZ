using adnumaZ.Common.Constants;
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

namespace adnumaZ.Areas.Administration.Controllers
{
    [Authorize(Roles = Constants.AdministratorRoleName)]
    [Area("Administration")]
    public class TorrentsController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public TorrentsController(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public IActionResult All()
        {
            var query = dbContext.Torrents
                .Include(x => x.Uploader)
                .Where(x => !x.IsApproved);

            var torrents = mapper.Map<List<TorrentViewModel>>(query
                .OrderByDescending(x => x.CreatedOn)
                .ThenByDescending(x => x.Id))
                .ToList();

            return View(torrents);
        }
    }
}
