using adnumaZ.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adnumaZ.ViewModels
{
    public class TorrentListViewModel
    {
        public List<TorrentViewModel> Torrents { get; set; }
        
        public Dictionary<string, TorrentSeedData> TorrentSeedData { get; set; }

        public int PreviousPage => CurrentPage - 1;

        public int CurrentPage { get; set; }

        public int NextPage => CurrentPage + 1;

        public int TorrentCount { get; set; }

        public int PagesCount { get; set; }

        public string Search { get; set; }
    }
}
