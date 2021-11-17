using adnumaZ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adnumaZ.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }

        public bool IsAdmin { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsBanned { get; set; }

        public DateTime? BannedOn { get; set; }

        public string BanReason { get; set; }

        public string BanReasonShortened => $"{BanReason.Substring(0, Math.Min(BanReason.Length, 15))}...";

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        // User 
        public string ImageUrl { get; set; }

        public double UploadedTorrentGBs { get; set; }

        public double DownloadedTorrentGBs { get; set; }

        public int UploadedTorrentsCount { get; set; }

        public virtual ICollection<Torrent> UploadedTorrents { get; set; }

        public int DownloadedTorrentsCount { get; set; }

        public virtual ICollection<Torrent> DownloadedTorrents { get; set; }

        public int FavouriteTorrentsCount { get; set; }

        public virtual ICollection<Torrent> FavouriteTorrents { get; set; }
    }
}
