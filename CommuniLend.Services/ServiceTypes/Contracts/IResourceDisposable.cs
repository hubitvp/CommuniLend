namespace CommuniLend.Services.ServiceTypes.Contracts
{
    public interface IResourceDisposable
    {

        Task DeleteUnusedFilesAsync(IEnumerable<string> files);
    }
}
