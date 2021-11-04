using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adnumaZ.ViewModels
{
    public class TorrentListViewModel
    {
        public List<TorrentViewModel> Torrents { get; set; }

        public int CurrentPage { get; set; }

        public int TorrentCount { get; set; }
        
        public int PagesCount { get; set; }

        public string Search { get; set; }
    }
}
