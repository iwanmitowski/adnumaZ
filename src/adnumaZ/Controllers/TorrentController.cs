using adnumaZ.Data;
using adnumaZ.Models;
using adnumaZ.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace adnumaZ.Controllers
{
    public class TorrentController : Controller
    {
        private const int TorrentsPerPage = 15;

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

        public async Task<IActionResult> ById(int id)
        {
            var torrent = mapper.Map<TorrentViewModel>(
                await dbContext.Torrents
                .Include(x => x.Uploader)
                .Include(x => x.Downloaders)
                .Include(x => x.Comments.OrderBy(x=>x.IsDeleted))
                .FirstOrDefaultAsync(x => x.Id == id));

            if (torrent == null)
            {
                return NotFound();
            }

            return View(torrent);
        }

        public IActionResult Download()
        {
            return RedirectToAction(nameof(this.All));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Download([FromForm] int? id)
        {
            //When user is logged out and tries to download torrent, he is asked to register and redirected to /Download
            //So the user is redirected to From Download to All
            if (id == null)
            {
                return RedirectToAction(nameof(this.Download));
            }

            var torrent = await dbContext.Torrents.FirstOrDefaultAsync(x => x.Id == id);

            if (torrent == null)
            {
                return RedirectToAction(nameof(All));
            }

            return PhysicalFile(torrent.TorrentFilePath, "application/octet-stream", torrent.Title + ".torrent");
        }

        [Authorize]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Upload(UploadTorrentViewModel torrentDTO)
        {
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

            var user = await userManager.GetUserAsync(HttpContext.User);

            try
            {
                using (Stream fileStream = new FileStream(saveToPath, FileMode.Create))
                {
                    await torrentDTO.File.CopyToAsync(fileStream);
                }

                user.UploadedTorrents.Add(torrent);

                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            //To torrents
            return RedirectToAction("Privacy", "Home");
        }

        public IActionResult All(int id, string search)
        {
            id = Math.Max(1, id);

            var skip = (id - 1) * TorrentsPerPage;

            var query = dbContext.Torrents
                .Include(x => x.Uploader)
                .Where(x => x.IsApproved);

            var words = search?
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Select(x => x.ToLower())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();

            if (words != null)
            {
                foreach (var word in words)
                {
                    query = query.Where(t => t.Title.ToLower().Contains(word) ||
                                            t.Description.ToLower().Contains(word));
                }
            }

            var torrents = mapper.Map<List<TorrentViewModel>>(query
                .OrderByDescending(x => x.ModifiedOn)
                .ThenBy(x => x.CreatedOn)
                .ThenByDescending(x => x.Id)
                .Skip(skip)
                .Take(TorrentsPerPage))
                .ToList();

            var torrentCount = query.Count();
            var pagesCount = (int)Math.Ceiling(torrentCount / (double)TorrentsPerPage);

            var viewModel = new TorrentListViewModel()
            {
                Torrents = torrents,
                CurrentPage = id,
                TorrentCount = torrentCount,
                PagesCount = pagesCount,
                Search = search,
            };

            return View(viewModel);
        }
    }
}
