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
        private readonly string BaseFilePath = Path.Combine("D:", "DemoCodes", "temp");

        [Required]
        public string Title { get; set; }

        //To set it
        public string TorrentFilePath => BaseFilePath;

        public IFormFile File { get; set; }

        public double Size { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string UserId { get; set; }
    }
}
