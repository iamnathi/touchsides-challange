using System;
using System.Threading;
using System.Threading.Tasks;

namespace Touchsides.Challange.Services
{
    public interface IFileService
    {
        Task<string> DownloadContentFromWebAsync(string bookUri, CancellationToken cancellationToken = default);
        Task<string> DownloadContentFromWebAsync(Uri bookUri, CancellationToken cancellationToken = default);

        Task<string> LoadContentFromFileSystemAsync(string bookUri, CancellationToken cancellationToken = default);
        Task<string> LoadContentFromFileSystemAsync(Uri bookUri, CancellationToken cancellationToken = default);
    }
}