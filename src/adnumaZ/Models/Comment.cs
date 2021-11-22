using adnumaZ.Common.Models.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace adnumaZ.Models
{
    public class Comment : IDeletableEntity, IAuditInfo
    {
        public Comment()
        {
            //this.Id = new Guid().ToString();
            this.ChildComments = new HashSet<Comment>();
        }
        public string Id { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        //Comment

        [Required]
        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int TorrentId { get; set; }

        public virtual Torrent Torrent { get; set; }

        public virtual string ParentId { get; set; }

        public virtual Comment Parent { get; set; }

        public virtual ICollection<Comment> ChildComments { get; set; }

    }
}
