using Microsoft.AspNetCore.Http;

namespace CommuniLend.Services.Contracts
{
    public interface IImageStorageService
    {

        Task<string?> SaveAsync(
            IFormFile? image,
            CancellationToken cancellationToken = default);

        Task DeleteAsync(
            string? imageUrl,
            CancellationToken cancellationToken = default);


        Task DeleteAsync(
            IEnumerable<string> imageUrls,
            CancellationToken cancellationToken = default);
    }
}
