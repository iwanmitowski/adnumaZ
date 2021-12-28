﻿using adnumaZ.Common.Constants;
using adnumaZ.Data;
using adnumaZ.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adnumaZ.Areas.Administration.Controllers
{
    [Authorize(Roles = Constants.AdministratorRoleName)]
    [Route("api/changeApproval")]
    [ApiController]
    public class ApproveTorrentAPIController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public ApproveTorrentAPIController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
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
    }
}