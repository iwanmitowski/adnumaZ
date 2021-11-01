using adnumaZ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adnumaZ.ViewModels
{
    public class TorrentAllViewModel
    {
        public int Id { get; set;
        }
        public string Title { get; set; }
        public string TitleShort { get; set; }

        public double Size { get; set; }

        public string Description { get; set; }

        public User Uploader { get; set; }

        public string CreatedOn { get; set; }
    }
}
