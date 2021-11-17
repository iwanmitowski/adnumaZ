using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adnumaZ.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalRegisteredUsers { get; set; }
        public int TotalBannedUsers { get; set; }
        public int TotalTorrents { get; set; }
        public double TotalUploadedGBs { get; set; }
        public double TotalDownloadedGBs { get; set; }
    }
}
