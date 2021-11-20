using adnumaZ.Common.Constants;
using adnumaZ.Data;
using adnumaZ.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks.Dataflow;

namespace adnumaZ.Areas.Administration.Controllers
{
    [Authorize(Roles = Constants.AdministratorRoleName)]
    [Area("Administration")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public DashboardController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
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
            //TODO: Add total comments

            var dashboardViewModel = new DashboardViewModel()
            {
                TotalRegisteredUsers = totalRegisteredUsers,
                TotalBannedUsers = totalBannedUsers,
                TotalTorrents = totalTorrents,
                TotalWaitingApproval = totalWaitingApproval,
                TotalUploadedGBs = totalUploadedGBs,
                TotalDownloadedGBs = totalDownloadedGBs,
            };

            return View(dashboardViewModel);
        }
    }
}
