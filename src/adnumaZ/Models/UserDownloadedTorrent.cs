using System;
using System.ComponentModel.DataAnnotations;

namespace adnumaZ.Models
{
    public class UserDownloadedTorrent
    {
        public UserDownloadedTorrent()
        {
            this.Id = Guid.NewGuid().ToString();
            this.DownloadedAt = DateTime.UtcNow;
        }

        public string Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int TorrentId { get; set; }
        public Torrent Torrent { get; set; }
        public DateTime DownloadedAt { get; set; }
    }
}
