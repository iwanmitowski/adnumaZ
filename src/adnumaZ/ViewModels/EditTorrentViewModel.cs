using System.ComponentModel.DataAnnotations;

namespace adnumaZ.ViewModels
{
    public class EditTorrentViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }
    }
}
