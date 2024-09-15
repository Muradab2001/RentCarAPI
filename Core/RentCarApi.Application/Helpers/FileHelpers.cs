using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Application.Helpers
{
    public static class FileHelpers
    {
        public static bool ImageIsOkay(this IFormFile file, int mb)
        {
            return file.Length / 1024 / 1024 < mb && file.ContentType.Contains("image/");
        }
        public static async Task<MemoryStream> ConvertToWebPAsync(this IFormFile file)
        {
            if (file == null || !file.ImageIsOkay(4)) 
            {
                throw new ArgumentException("Invalid file. File is either null or not a valid image.");
            }

            using (var image = await Image.LoadAsync(file.OpenReadStream()))
            {
                var encoder = new WebpEncoder
                {
                    Quality = 75 
                };

                var memoryStream = new MemoryStream();
                image.Save(memoryStream, encoder);
                memoryStream.Position = 0; 
                return memoryStream;
            }
        }
    }
}
