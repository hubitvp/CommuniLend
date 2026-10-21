using CommuniLend.Services.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace CommuniLend.Services
{
    public class LocalImageStorageService : IImageStorageService
    {
        protected readonly IWebHostEnvironment webHostEnvironment;

        protected readonly string storagePath;

        protected readonly string[] folderChain = new[] { "images", "tools" };


        public LocalImageStorageService(IWebHostEnvironment environment)
        {
            webHostEnvironment = environment;

            storagePath = webHostEnvironment.WebRootPath;

            foreach (var folder in folderChain)
            {
                storagePath = Path.Combine(storagePath, folder);
            }
        }



        public async Task<string?> SaveAsync(IFormFile? image, CancellationToken cancellationToken = default)
        {
            if (image == null)
            {
                return null;
            }


            string fileName = image.FileName;

            var imageFullStoragePath = Path.Combine(storagePath, fileName);

            while (File.Exists(imageFullStoragePath))
            {
                fileName = $"{Guid.NewGuid():N}-{fileName}";

                imageFullStoragePath = Path.Combine(storagePath, fileName);
            }

            await using var stream = new FileStream(
                imageFullStoragePath,
                FileMode.CreateNew,
                FileAccess.Write,
                FileShare.None,
                bufferSize: 50 * 1024,
                useAsync: true);

            await image.CopyToAsync(stream, cancellationToken);

            return $"/{string.Join("/", folderChain)}/{fileName}";
        }



        public async Task DeleteAsync(string? imageUrl, CancellationToken cancellationToken = default)
        {
            if (imageUrl == null)
            {
                return;
            }

            var fileName = Path.GetFileName(imageUrl);

            if ("no-image.jpg" == fileName)
            {
                return;
            }

            var filePath = Path.Combine(storagePath, fileName);


            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                }
                catch
                {
                }
            }

        }


        public async Task DeleteAsync(IEnumerable<string> imageUrls, CancellationToken cancellationToken = default)
        {
            foreach (var url in imageUrls)
            {
                await DeleteAsync(url, cancellationToken);
            }
        }
    }
}
