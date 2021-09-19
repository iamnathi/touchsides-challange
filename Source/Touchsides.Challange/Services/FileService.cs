using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Touchsides.Challange.Services
{
    public class FileService : IFileService, IDisposable
    {
        private ILogger _logger;
        private readonly HttpClientHandler _httpClientHandler;        

        public FileService(ILogger logger)
        {
            _logger = logger;
            _httpClientHandler = new HttpClientHandler();
        }


        public async Task<string> DownloadContentFromWebAsync(string fileUri, CancellationToken cancellationToken = default)
        {
            return await DownloadContentFromWebAsync(new Uri(fileUri), cancellationToken);
        }

        public async Task<string> DownloadContentFromWebAsync(Uri fileUri, CancellationToken cancellationToken = default)
        {
            using var httpClient = new HttpClient(_httpClientHandler, false);
            try
            {
                var response = await httpClient.GetAsync(fileUri);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"An unexpected error caused the operation to fail to download file contents from {fileUri}");
                return string.Empty;
            }
        }

        public async Task<string> LoadContentFromFileSystemAsync(string filePath, CancellationToken cancellationToken = default)
        {
            return await LoadContentFromFileSystemAsync(new Uri(filePath), cancellationToken);
        }

        public async Task<string> LoadContentFromFileSystemAsync(Uri bookUri, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => string.Empty);
        }

        public void Dispose()
        {
            _httpClientHandler?.Dispose();
        }        
    }
}