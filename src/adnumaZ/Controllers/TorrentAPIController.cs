using System;
using System.Threading.Tasks;

using adnumaZ.Common.Constants;
using adnumaZ.Data;
using adnumaZ.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace adnumaZ.Controllers
{
    
    [ApiController]
    public class TorrentAPIController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public TorrentAPIController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
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
        public async Task<ActionResult<ApproveTorrentResponseModel>> ChangeFavourability(ApproveTorrentInputModel input)
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
    }
}
