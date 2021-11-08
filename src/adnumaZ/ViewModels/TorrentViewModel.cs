using adnumaZ.Models;

namespace adnumaZ.ViewModels
{
    public class TorrentViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string TitleShort { get; set; }

        public double Size { get; set; }

        public string Description { get; set; }

        public User Uploader { get; set; }

        public string CreatedOn { get; set; }

        public bool IsApproved { get; set; }
    }
}
