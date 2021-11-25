﻿using adnumaZ.Models;
using System.Collections.Generic;

namespace adnumaZ.ViewModels
{
    public class TorrentViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string TitleShort { get; set; }

        public double Size { get; set; }

        public string TorrentFilePath { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string DescriptionShort { get; set; }

        public User Uploader { get; set; }

        public string CreatedOn { get; set; }

        public bool IsApproved { get; set; }

        public int DownloadersCount { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}
