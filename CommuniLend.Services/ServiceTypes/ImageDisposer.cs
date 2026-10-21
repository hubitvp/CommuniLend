using CommuniLend.Services.ServiceTypes.Contracts;
using Microsoft.AspNetCore.Hosting;

namespace CommuniLend.Services.ServiceTypes
{
    public abstract class ImageDisposer : IResourceDisposable
    {
        protected readonly IWebHostEnvironment webHostEnvironment;

        protected ImageDisposer(IWebHostEnvironment environment)
        {
            webHostEnvironment = environment;
        }

        public virtual async Task DeleteUnusedFilesAsync(IEnumerable<string> files)
        {
            foreach (var file in files)
            {
                var filePath = Path.Combine(
                    webHostEnvironment.WebRootPath,
                    file.TrimStart('/', '\\'));

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }

    }
}
