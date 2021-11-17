using adnumaZ.Common.Constants;
using adnumaZ.Data;
using adnumaZ.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
            var totalRegisteredUsers = dbContext.UserAccounts.Count();
            var totalBannedUsers = dbContext.UserAccounts.Count(x => x.IsBanned);
            var totalTorrents = dbContext.Torrents.Count();
            var totalUploadedGBs = dbContext.UserAccounts.Sum(x => x.UploadedTorrentGBs);
            var totalDownloadedGBs = dbContext.UserAccounts.Sum(x => x.DownloadedTorrentGBs);
            //TODO: Add total comments

            var dashboardViewModel = new DashboardViewModel()
            {
                TotalRegisteredUsers = totalRegisteredUsers,
                TotalBannedUsers = totalBannedUsers,
                TotalTorrents = totalTorrents,
                TotalUploadedGBs = totalUploadedGBs,
                TotalDownloadedGBs = totalDownloadedGBs,
            };

            return View(dashboardViewModel);
        }
    }
}
