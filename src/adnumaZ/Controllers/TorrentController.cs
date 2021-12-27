using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using adnumaZ.Areas.Administration.Controllers;
using adnumaZ.Common.Constants;
using adnumaZ.Data;
using adnumaZ.Models;
using adnumaZ.Services.TorrentService.Contracts;
using adnumaZ.ViewModels;

using AutoMapper;

using BencodeNET.Parsing;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace adnumaZ.Controllers
{
    public class TorrentController : Controller
    {
        private const int TorrentsPerPage = 15;

        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;
        private readonly ITorrentService torrentService;
        private readonly IBencodeParser bencodeParser;
        private readonly string TorrentsDirectory;

        public TorrentController(ApplicationDbContext dbContext,
            IMapper mapper,
            UserManager<User> userManager,
            IConfiguration configuration,
            ITorrentService torrentService,
            IBencodeParser bencodeParser)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.userManager = userManager;
            this.configuration = configuration;
            this.torrentService = torrentService;
            this.bencodeParser = bencodeParser;

            var torrentsDirectory = Environment.GetEnvironmentVariable("FILE_UPLOAD_DIRECTORY");
            if (torrentsDirectory == null)
            {
                torrentsDirectory = Path.Combine("D:", "DemoCodes", "temp");
            }

            TorrentsDirectory = torrentsDirectory;
        }

        public async Task<IActionResult> ById(int id)
        {
            var torrent = mapper.Map<TorrentViewModel>(
                await dbContext.Torrents
                .Include(x => x.Uploader)
                .Include(x => x.Downloaders)
                .Include(x=>x.FavouritedByUsers)
                .Include(x => x.Comments.OrderBy(x => x.IsDeleted).ThenByDescending(x => x.CreatedOn))
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

            var torrent = await dbContext.Torrents.FindAsync(id);
            var user = await userManager.GetUserAsync(HttpContext.User);

            user.DownloadedTorrents.Add(torrent);

            await dbContext.SaveChangesAsync();

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

            try
            {
                var torrentStream = torrentDTO.File.OpenReadStream();
                var torrentObject = await torrentService.GetTorrentObjectAsync(torrentStream);

                var torrent = mapper.Map<Torrent>(torrentDTO);
                torrent.Size = torrentService.SetTorrentSize(torrentObject);
                torrent.Hash = torrentService.SetTorrentHash(torrentObject);

                await dbContext.AddAsync(torrent);

                var fileName = torrentService.GenerateFileName();
                var saveToPath = Path.Combine(TorrentsDirectory, fileName);

                torrentService.SetTorrentFilePath(torrent, saveToPath);

                await torrentService.CreateTorrentInTheGivenDirectory(saveToPath, torrentDTO);

                var user = await userManager.GetUserAsync(HttpContext.User);

                torrentService.AsignTorrentToUser(torrent, user);

                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            //To torrents
            return RedirectToAction(nameof(this.All),
                                   nameof(TorrentController).Replace("Controller", string.Empty),
                                   new { id = 1, search = string.Empty });
        }

        public IActionResult All(int id, string search)
        {
            id = Math.Max(1, id);

            var skip = Math.Abs((id - 1) * TorrentsPerPage);

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

            if (id > pagesCount)
            {
                return RedirectToAction(nameof(this.All), new { id = pagesCount, search = search });
            }

            var trackerApiPath = configuration["TrackerApiPath"];
            var torrentSeedDataTasks = torrentService.GetTorrentSeedData(trackerApiPath, torrents);

            var torrentSeedData = torrentSeedDataTasks
                .ToDictionary(t => t.Result.Hash, t => t.Result);

            var viewModel = new TorrentListViewModel()
            {
                Torrents = torrents,
                TorrentSeedData = torrentSeedData,
                CurrentPage = id,
                TorrentCount = torrentCount,
                PagesCount = pagesCount,
                Search = search,
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Favourite(int id)
        {
            id = Math.Max(1, id);

            var skip = Math.Abs((id - 1) * TorrentsPerPage);

            var user = await userManager.GetUserAsync(HttpContext.User);

            var query = dbContext.Torrents
                .Include(x => x.Uploader)
                .Include(x => x.FavouritedByUsers)
                .Where(x => x.IsApproved)
                .Where(x => x.FavouritedByUsers.Any(x=>x.Id == user.Id));

            var qString = query.ToQueryString();

            var torrents = mapper.Map<List<TorrentViewModel>>(query
                .OrderByDescending(x => x.ModifiedOn)
                .ThenBy(x => x.CreatedOn)
                .ThenByDescending(x => x.Id)
                .Skip(skip)
                .Take(TorrentsPerPage))
                .ToList();

            var torrentCount = query.Count();
            var pagesCount = (int)Math.Ceiling(torrentCount / (double)TorrentsPerPage);

            if (id > pagesCount)
            {
                return RedirectToAction(nameof(this.Favourite), new { id = pagesCount});
            }

            var trackerApiPath = configuration["TrackerApiPath"];
            var torrentSeedDataTasks = torrentService.GetTorrentSeedData(trackerApiPath, torrents);

            var torrentSeedData = torrentSeedDataTasks
                .ToDictionary(t => t.Result.Hash, t => t.Result);

            var viewModel = new TorrentListViewModel()
            {
                Torrents = torrents,
                TorrentSeedData = torrentSeedData,
                CurrentPage = id,
                TorrentCount = torrentCount,
                PagesCount = pagesCount,
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var torrent = mapper.Map<EditTorrentViewModel>(await dbContext.Torrents.FindAsync(id));

            return PartialView("_EditTorrentPartial", torrent);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTorrentViewModel model)
        {
            var torrent = await dbContext.Torrents.FindAsync(model.Id);

            torrent.ModifiedOn = DateTime.UtcNow;
            torrent.Title = model.Title;
            torrent.Description = model.Description;
            torrent.ImageUrl = model.ImageUrl;
            torrent.IsApproved = false;

            await dbContext.SaveChangesAsync();

            if (this.User.IsInRole(Constants.AdministratorRoleName))
            {
                return RedirectToAction(nameof(TorrentsController.All),
                                        nameof(TorrentsController).Replace("Controller", string.Empty),
                                        new { area = "Administration" });
            }

            return View(nameof(this.All));
        }
    }
}
