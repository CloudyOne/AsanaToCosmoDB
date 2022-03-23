using AsanaToCosmoDB.Models;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Azure.Cosmos;
using System.IO;
using System.Threading.Tasks;

namespace AsanaToCosmoDB.Services
{
    public class AzureStorageService
    {
        private BlobContainerClient Client;
        private AzureStorageConfig Config;
        private Database Database { get; set; }
        public string BaseUri { get { return Client.Uri.AbsoluteUri; } }
        public string ContainerName { get { return Config.ContainerName; } }
        public AzureStorageService(AzureStorageConfig config)
        {
            Config = config;
            Client = new BlobServiceClient(Config.ConnectionString).GetBlobContainerClient(Config.ContainerName);
        }

        public async Task<BlobContentInfo> Upload(string blobId, MemoryStream stream)
        {
            return await Client.GetBlobClient(blobId).UploadAsync(stream, true);
        }
    }
}
