using adnumaZ.Common.Attributes;
using adnumaZ.Common.Helpers;
using adnumaZ.Data;
using adnumaZ.Models;
using adnumaZ.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace adnumaZ.Controllers
{
    public class TorrentController : Controller
    {
        private readonly string[] permittedExtensions = { ".torrent" };
        private const long fileSizeLimit = 5242880000; //5 GB
        private static readonly FormOptions defaultFormOptions = new FormOptions();

        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<TorrentController> logger;
        private readonly IMapper mapper;

        public TorrentController(ApplicationDbContext dbContext,
            ILogger<TorrentController> logger,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
        }

        public IActionResult ById(int id)
        {
            return View();
        }

        public IActionResult Upload(int? id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload()
        {
            //Todo fix it with large files.
            //Save it to db as entity
            //Name the file properly

            var request = HttpContext.Request;

            // validation of Content-Type
            // 1. first, it must be a form-data request
            // 2. a boundary should be found in the Content-Type
            if (!request.HasFormContentType ||
                !MediaTypeHeaderValue.TryParse(request.ContentType, out var mediaTypeHeader) ||
                string.IsNullOrEmpty(mediaTypeHeader.Boundary.Value))
            {
                return new UnsupportedMediaTypeResult();
            }

            var reader = new MultipartReader(mediaTypeHeader.Boundary.Value, request.Body);
            var section = await reader.ReadNextSectionAsync();

            while (section != null)
            {
                var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition,
                    out var contentDisposition);

                if (hasContentDispositionHeader && contentDisposition.DispositionType.Equals("form-data") &&
                    !string.IsNullOrEmpty(contentDisposition.FileName.Value))
                {
                    var fileName = "File" + DateTime.Now.Second + ".torrent";
                    var saveToPath = Path.Combine("D:", "DemoCodes", "temp", fileName);

                    using (var targetStream = System.IO.File.Create(saveToPath))
                    {
                        await section.Body.CopyToAsync(targetStream);
                    }

                    //Change it
                    return RedirectToAction("Privacy", "Home");
                }

                section = await reader.ReadNextSectionAsync();
            }

            // If the code runs to this location, it means that no files have been saved
            return BadRequest("No files data in the request.");
        }

        public IActionResult All()
        {
            return View();
        }
    }
}
