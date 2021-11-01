using adnumaZ.Data;
using adnumaZ.Models;
using adnumaZ.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace adnumaZ.Controllers
{
    public class TorrentController : Controller
    {
        private readonly string[] permittedExtensions = { ".torrent" };
        private static readonly FormOptions defaultFormOptions = new FormOptions();

        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public TorrentController(ApplicationDbContext dbContext,
            IMapper mapper,
            UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public IActionResult ById(int id)
        {
            return View(id);
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Upload(UploadTorrentViewModel torrentDTO)
        {
            //Todo
            //Inject usermanager in automapper so the user is automatically mapped

            if (!ModelState.IsValid)
            {
                return View(torrentDTO);
            }

            var torrent = mapper.Map<Torrent>(torrentDTO);

            await dbContext.AddAsync(torrent);

            int fileNumber = dbContext.Torrents.Count() + 1;
            var fileName = "File" + fileNumber + ".torrent";
            var saveToPath = Path.Combine("D:", "DemoCodes", "temp", fileName);

            torrent.TorrentFilePath = saveToPath;
            torrent.Uploader = userManager.GetUserAsync(this.User).Result;

            await dbContext.SaveChangesAsync();

            using (Stream fileStream = new FileStream(saveToPath, FileMode.Create))
            {
                await torrentDTO.File.CopyToAsync(fileStream);
            }

            //To torrents
            return RedirectToAction("Privacy", "Home");
        }

        public IActionResult All()
        {
            var allTorrents = mapper.Map<List<TorrentAllViewModel>>(
                dbContext.Torrents.Include(x=>x.Uploader));
            return View(allTorrents);
        }
    }
}
