using adnumaZ.Common.Constants;
using adnumaZ.Common.Models.Contracts;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace adnumaZ.Models
{
    public class Torrent : IAuditInfo, IDeletableEntity
    {
        public Torrent()
        {
            this.Comments = new HashSet<Comment>();
            this.ImageUrl = Constants.DefaultTorrentImage;
            this.UserDownloadedTorrents = new HashSet<UserDownloadedTorrent>();
            this.UserFavouritedTorrents = new HashSet<UserFavouritedTorrent>();
        }
        public int Id { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public bool IsApproved { get; set; }

        // Torrent

        [Required]
        public string Title { get; set; }

        [Required]
        public string TorrentFilePath { get; set; }

        public double Size { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string Hash { get; set; }

        [InverseProperty(nameof(User.Id))]
        public string UserId { get; set; }

        [InverseProperty(nameof(User.UploadedTorrents))]
        public virtual User Uploader { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<UserDownloadedTorrent> UserDownloadedTorrents { get; set; }

        public virtual ICollection<UserFavouritedTorrent> UserFavouritedTorrents { get; set; }

    }
}
