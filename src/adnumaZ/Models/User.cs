using adnumaZ.Common.Constants;
using adnumaZ.Common.Models.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace adnumaZ.Models
{
    public class User : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public User()
        {
            //this.Id = Guid.NewGuid().ToString();
            this.UploadedTorrents = new HashSet<Torrent>();
            this.DownloadedTorrents = new HashSet<Torrent>();
            this.FavouriteTorrents = new HashSet<Torrent>();
            this.ImageUrl = Constants.DefaultProfilePfp;
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

        public virtual ICollection<Torrent> UploadedTorrents { get; set; }

        public virtual ICollection<Torrent> DownloadedTorrents { get; set; }

        public virtual ICollection<Torrent> FavouriteTorrents { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
