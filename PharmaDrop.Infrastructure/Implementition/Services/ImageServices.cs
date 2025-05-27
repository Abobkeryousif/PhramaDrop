using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using PharmaDrop.Application.Contract.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaDrop.Infrastructure.Implementition.Services
{
    public class ImageServices : IImageServices
    {
        private readonly IFileProvider fileProvider;

        public ImageServices(IFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;
        }

        public async Task<List<string>> AddImageAsync(IFormFileCollection files, string src)
        {
            var SaveImageSrc = new List<string>();
            var ImageDirctory = Path.Combine("wwwroot","Images",src);
            if(Directory.Exists(ImageDirctory) is not true)
            {
                Directory.CreateDirectory(ImageDirctory);
            }

            foreach (var item in files)
            {
                if (item.Length > 0)
                {
                    var imageName = item.FileName;
                    var imageSrc = $"/Images/{src}/{imageName}";
                    var root = Path.Combine (ImageDirctory, imageName);

                    using (FileStream fileStream = new FileStream(root, FileMode.Create)) 
                    {
                         await item.CopyToAsync(fileStream);
                    }
                    SaveImageSrc.Add(imageSrc);
                }
            }
            return SaveImageSrc; 
        }

        public void DeleteImageAsync(string src)
        {
            var info = fileProvider.GetFileInfo(src);
            var root = info.PhysicalPath;
            File.Delete(root);

        }
    }
}
