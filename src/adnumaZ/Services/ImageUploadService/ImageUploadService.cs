using System;
using System.IO;
using System.Threading.Tasks;

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

using Microsoft.AspNetCore.Http;

namespace adnumaZ.Services.ImageUploadService
{
    public static class ImageUploadService
    {
        public static async Task<string> UploadImageAsync(Cloudinary cloudinary, IFormFile image)
        {
            byte[] imageBytes;
            string imageUrl;

            using (var memoryStream = new MemoryStream())
            {
                await image.CopyToAsync(memoryStream);
                imageBytes = memoryStream.ToArray();

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(Guid.NewGuid().ToString(), memoryStream)
                };

                var result = await cloudinary.UploadAsync(uploadParams);

                imageUrl = result.Url.AbsoluteUri;
            }

            return imageUrl;
        }
    }
}
