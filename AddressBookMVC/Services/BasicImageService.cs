using AddressBookMVC.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookMVC.Services
{
    public class BasicImageService : IImageService
    {
        public async Task<byte[]> EncodeImageAsync(IFormFile file)
        {
            if (file is null)
            {
                return null;
            }

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            return ms.ToArray();
        }

        public async Task<byte[]> EncodeImageAsync(string fileName)
        {
            var file = $"{Directory.GetCurrentDirectory()}/wwwroot/img/(fileName)";
            return await File.ReadAllBytesAsync(file);
        }

        public string DecodeImage(byte[] data, string type)
        {
            if (data is null || type is null)
            {
                return null;
            }
            return $"data:{type};base64,{Convert.ToBase64String(data)}";
        }

        public string ContentType(IFormFile file)
        {
            if (file is null)
            {
                return null;
            }

            return file.ContentType;
        }

        public int Size(IFormFile file)
        {
            {
                if (file is null)
                {
                    return 0;
                }
                return Convert.ToInt32(file.Length);
            }
        }
    }
}
