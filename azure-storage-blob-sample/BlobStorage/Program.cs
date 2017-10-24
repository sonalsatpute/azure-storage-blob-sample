using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;

namespace BlobStorage
{
  class Program
  {
    static void Main(string[] args)
    {
    }

    static void Upload()
    {
      var connectionString = System.Configuration.ConfigurationManager.AppSettings["StorageConnectionString"];
      CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
      CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
      CloudBlobContainer blobContainer = blobClient.GetContainerReference("testcontainer");
      blobContainer.CreateIfNotExists();

      CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference("532_OD_Changes.pdf");

      using (var fileStream = File.OpenRead(@"D:\temp\532_OD_Changes.pdf"))
      {
        blockBlob.UploadFromStream(fileStream);
      }
    }
  }
}
