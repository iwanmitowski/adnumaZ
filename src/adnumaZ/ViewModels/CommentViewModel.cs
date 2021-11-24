using adnumaZ.Models;
using System;
using System.Collections.Generic;

namespace adnumaZ.ViewModels
{
    public class CommentViewModel
    {
        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public string UserUserName { get; set; }

        public string UserImageUrl { get; set; }

        public int TorrentId { get; set; }

        public virtual Torrent Torrent { get; set; }

        public virtual string ParentId { get; set; }

        public virtual Comment Parent { get; set; }

        public virtual ICollection<Comment> ChildComments { get; set; }
    }
}
