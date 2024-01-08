using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionForAuth
{
    //    public static class Function1
    //    {
    //        [FunctionName("Function1")]
    //public static async Task<IActionResult> Run(
    //    [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
    //    ILogger log)
    //{
    //    log.LogInformation("C# HTTP trigger function processed a request.");

    //    string name = req.Query["name"];

    //    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    //    dynamic data = JsonConvert.DeserializeObject(requestBody);
    //    name = name ?? data?.name;

    //    string responseMessage = string.IsNullOrEmpty(name)
    //        ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
    //        : $"Hello, {name}. This HTTP triggered function executed successfully.";

    //    return new OkObjectResult(responseMessage);
    //}
    //  }

    public static class StoreTextInBlob
    {
        [FunctionName("StoreTextInBlob")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                // Your Azure Storage connection string
                string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=storageaccountdakshi;AccountKey=kiNa8l+SxTNy9Khrt3XCEqrGrJoRdflorv0RTL5N8C+Oco+SKdtS0U+p0kdpqE0Kykc9IA3RWVQl+AStUIUZtQ==;EndpointSuffix=core.windows.net";
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
