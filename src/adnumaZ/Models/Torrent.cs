using adnumaZ.Common.Models.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace adnumaZ.Models
{
    public class Torrent : IAuditInfo, IDeletableEntity
    {
        public Torrent()
        {
            this.Downloaders = new HashSet<User>();
            this.FavouritedByUsers = new HashSet<User>();
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

        [InverseProperty(nameof(User.Id))]
        public string UserId { get; set; }

        public virtual User Uploader { get; set; }

        [InverseProperty(nameof(User.DownloadedTorrents))]
        public virtual ICollection<User> Downloaders { get; set; }

        [InverseProperty(nameof(User.FavouriteTorrents))]
        public virtual ICollection<User> FavouritedByUsers { get; set; }
    }
}
