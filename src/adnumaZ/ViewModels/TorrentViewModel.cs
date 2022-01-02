using adnumaZ.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace adnumaZ.ViewModels
{
    public class TorrentViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string TitleShort { get; set; }

        public double Size { get; set; }

        public string TorrentFilePath { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string DescriptionShort { get; set; }

        public User Uploader { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsApproved { get; set; }

        public long DownloadersCount => UserDownloadedTorrents.Select(x=>x.User).Count();

        public string Hash { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public IEnumerable<string> FavouritedByUsersId => UserFavouritedTorrents.Select(uft => uft.User.Id).Distinct();

        public IEnumerable<UserDownloadedTorrent> UserDownloadedTorrents { get; set; }

        public IEnumerable<UserFavouritedTorrent> UserFavouritedTorrents { get; set; }

        public bool IsFavourited { get; set; }
    }
}
