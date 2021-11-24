using adnumaZ.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace adnumaZ.ViewModels
{
    public class CommentInputModel
    {
        public string ParentId { get; set; }

        [Required]
        public string Content { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public int TorrentId { get; set; }

        public Torrent Torrent { get; set; }

    }
}
