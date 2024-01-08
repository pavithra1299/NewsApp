using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;

namespace FunctionApp1
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                // Your Azure Storage connection string
                string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=newsstoragep;AccountKey=e4M3PJIPu1Eyi9dtX7KbDKZUN3u4raTWFTt2CKwYrA1C5qMVUE1mTxWjdBER2ohlL9f/wprm3br1+ASttgJNYw==;EndpointSuffix=core.windows.net";

                // Retrieve storage account from connection string
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);

                // Create a blob client
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Retrieve a reference to a container
                CloudBlobContainer container = blobClient.GetContainerReference("message");

                // Create the container if it doesn't exist
                await container.CreateIfNotExistsAsync();

                // Define a unique name for the blob
                string blobName = $"{Guid.NewGuid()}.txt";

                // Retrieve reference to a blob
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);

                // Upload text to the blob
                await blockBlob.UploadTextAsync(requestBody);

                return new OkObjectResult($"Text stored in blob: {blobName}");
            }
            catch (Exception ex)
            {
                log.LogError($"Error storing text in blob: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}