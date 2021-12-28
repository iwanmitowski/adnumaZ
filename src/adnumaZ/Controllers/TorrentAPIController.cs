using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

using adnumaZ.Common.Constants;
using adnumaZ.Data;
using adnumaZ.Models;
using adnumaZ.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace adnumaZ.Controllers
{
    [ApiController]
    public class TorrentAPIController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<User> userManager;

        public TorrentAPIController(ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize(Roles = Constants.AdministratorRoleName)]
        [Route("api/changeApproval")]
        public async Task<ActionResult<ApproveTorrentResponseModel>> ChangeApproval(ApproveTorrentInputModel input)
        {
            var torrent = await dbContext.Torrents.FindAsync(input.TorrentId);

            if (torrent == null)
            {
                return NotFound();
            }

            torrent.IsApproved = !torrent.IsApproved;
            torrent.ModifiedOn = DateTime.UtcNow;
            await dbContext.SaveChangesAsync();

            return new ApproveTorrentResponseModel() { IsApproved = torrent.IsApproved };
        }

        [HttpPost]
        [Authorize]
        [Route("api/changeFavourability")]
        public async Task<ActionResult<FavouriteTorrentResponseModel>> ChangeFavourability(FavouriteTorrentInputModel input)
        {
            var torrent = await dbContext.Torrents
                .Include(x => x.UserFavouritedTorrents)
                .FirstOrDefaultAsync(x => x.Id == input.TorrentId);

            if (torrent == null)
            {
                return NotFound();
            }

            var currentUser = await userManager.GetUserAsync(HttpContext.User);

            var user = await dbContext.UserAccounts.FindAsync(currentUser.Id);
            var uft = torrent.UserFavouritedTorrents.FirstOrDefault(uft => uft.User == user && uft.Torrent == torrent);

            if (uft != null)
            {
                torrent.UserFavouritedTorrents.Remove(uft);
            }
            else
            {
                uft = new UserFavouritedTorrent()
                {
                    User = user,
                    Torrent = torrent,
                };

                torrent.UserFavouritedTorrents.Add(uft);
            }

            await dbContext.SaveChangesAsync();

            return new FavouriteTorrentResponseModel() { TorrentId = torrent.Id };
        }
    }
}
