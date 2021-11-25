using adnumaZ.Common.Constants;
using adnumaZ.Data;
using adnumaZ.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Dataflow;

namespace adnumaZ.Areas.Administration.Controllers
{
    [Authorize(Roles = Constants.AdministratorRoleName)]
    [Area("Administration")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public DashboardController(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            var userAccounts = dbContext.UserAccounts;
            var torrents = dbContext.Torrents;

            var totalRegisteredUsers = userAccounts.Count();
            var totalBannedUsers = userAccounts.Count(x => x.IsBanned);
            var totalTorrents = torrents.Count();
            var totalWaitingApproval = torrents.Count(x => !x.IsApproved);
            var totalUploadedGBs = dbContext.UserAccounts.Sum(x => x.UploadedTorrentGBs);
            var totalDownloadedGBs = dbContext.UserAccounts.Sum(x => x.DownloadedTorrentGBs);
            var allComments = mapper.Map<IEnumerable<CommentViewModel>>(dbContext.Comments.OrderByDescending(x => x.CreatedOn).ThenByDescending(x => x.ModifiedOn));
            //TODO: Add total comments

            var dashboardViewModel = new DashboardViewModel()
            {
                TotalRegisteredUsers = totalRegisteredUsers,
                TotalBannedUsers = totalBannedUsers,
                TotalTorrents = totalTorrents,
                TotalWaitingApproval = totalWaitingApproval,
                TotalUploadedGBs = totalUploadedGBs,
                TotalDownloadedGBs = totalDownloadedGBs,
                AllComments = allComments,
            };

            return View(dashboardViewModel);
        }
    }
}
