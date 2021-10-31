using adnumaZ.Common.Аttributes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace adnumaZ.ViewModels
{
    public class UploadTorrentViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public IFormFile File { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string UserId { get; set; }

        [GoogleReCaptchaValidation]
        public string RecaptchaValue { get; set; }
    }
}
