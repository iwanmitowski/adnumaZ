using System;

namespace adnumaZ.Models
{
    public class UserFavouritedTorrent
    {
        public UserFavouritedTorrent()
        {
            this.FavouritedAt = DateTime.UtcNow;
        }
        public string UserId { get; set; }
        public User User { get; set; }
        public int TorrentId { get; set; }
        public Torrent Torrent { get; set; }
        public DateTime FavouritedAt { get; set; }
    }
}
