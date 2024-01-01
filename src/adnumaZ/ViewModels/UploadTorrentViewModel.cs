using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using adnumaZ.Common.Аttributes;

namespace adnumaZ.ViewModels
{
    public class UploadTorrentViewModel
    {
        [Required]
        [MaxLength(80)]
        public string Title { get; set; }

        [Required]
        public IFormFile File { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string UserId { get; set; }

        [GoogleReCaptchaValidation]
        public string RecaptchaValue { get; set; }
    }
}
