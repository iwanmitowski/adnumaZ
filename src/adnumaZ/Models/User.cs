using adnumaZ.Common.Constants;
using adnumaZ.Common.Models.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace adnumaZ.Models
{
    public class User : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public User()
        {
            this.UploadedTorrents = new HashSet<Torrent>();
            this.ImageUrl = Constants.DefaultProfilePfp;
            this.UserDownloadedTorrents = new HashSet<UserDownloadedTorrent>();
            this.UserFavouritedTorrents = new HashSet<UserFavouritedTorrent>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [Required]
        public bool IsBanned { get; set; }

        public DateTime? BannedOn { get; set; }

        public string BanReason { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        // User 
        public string ImageUrl { get; set; }

        public double UploadedTorrentGBs { get; set; }

        public double DownloadedTorrentGBs { get; set; }

        [InverseProperty(nameof(Torrent.Uploader))]
        public virtual ICollection<Torrent> UploadedTorrents { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<UserDownloadedTorrent> UserDownloadedTorrents { get; set; }
        public virtual ICollection<UserFavouritedTorrent> UserFavouritedTorrents { get; set; }

    }
}
