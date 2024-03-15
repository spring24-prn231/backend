using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BusinessObjects.Common.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Services.Implements
{
    public class AzureBlobService
    {
        private readonly BlobServiceClient _blobClient;
        private readonly BlobContainerClient _containerImagesClient;
        private readonly BlobContainerClient _containerContractsClient;
        private readonly IConfiguration _configuration;
        public AzureBlobService(IConfiguration configuration)
        {
            _configuration = configuration;
            _blobClient = new BlobServiceClient(_configuration["BlobKey"]);
            _containerImagesClient = _blobClient.GetBlobContainerClient("images");
            _containerContractsClient = _blobClient.GetBlobContainerClient("contracts");
        }

        public async Task<List<string>> UploadFiles(List<IFormFile> files, string fileName,  StorageType storageType = StorageType.Image)
        {
            var fileLinkResponse = new List<string>();
            foreach (var file in files)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    memoryStream.Position = 0;
                    BlobContainerClient? containerClient = storageType == StorageType.Image ? _containerImagesClient : _containerContractsClient;
                    var container = storageType == StorageType.Image ? "images" : "contracts";
                    var blobClient = containerClient.GetBlobClient(fileName);
                    await blobClient.UploadAsync(memoryStream, overwrite: true);
                    fileLinkResponse.Add($"https://birthdayblitzfilestorage.blob.core.windows.net/{container}/{fileName}");
                }
            };

            return fileLinkResponse;
        }

        public async Task<List<BlobItem>> GetUploadedBlobs()
        {
            var items = new List<BlobItem>();
            var uploadedFiles = _containerImagesClient.GetBlobsAsync();
            await foreach (BlobItem file in uploadedFiles)
            {
                items.Add(file);
            }

            return items;
        }
    }
}
