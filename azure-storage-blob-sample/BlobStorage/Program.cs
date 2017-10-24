using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;

namespace BlobStorage
{
  class Program
  {
    static void Main(string[] args)
    {
      var connectionString = System.Configuration.ConfigurationManager.AppSettings["StorageConnectionString"];
      CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
      CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
      CloudBlobContainer blobContainer = blobClient.GetContainerReference("testcontainer");

      //Upload(blobContainer);
      //ListAttributes(blobContainer);
      SetMetadata(blobContainer);
      ListMetadata(blobContainer);

      Console.ReadLine();

    }

    static void Upload(CloudBlobContainer container)
    {
      container.CreateIfNotExists();

      CloudBlockBlob blockBlob = container.GetBlockBlobReference("532_OD_Changes.pdf");

      using (var fileStream = File.OpenRead(@"D:\temp\532_OD_Changes.pdf"))
      {
        blockBlob.UploadFromStream(fileStream);
      }
    }

    static void ListAtrribute(CloudBlobContainer container)
    {
      container.FetchAttributes();

      Console.WriteLine($"Container Name: {container.StorageUri.PrimaryUri}");
      Console.WriteLine($"Last Modified: {container.Properties.LastModified}");
    }

    static void SetMetadata(CloudBlobContainer container)
    {
      container.Metadata.Clear();
      container.Metadata.Add("Auther", "Sonal Satpute");
      container.Metadata["AutheredOn"] = "Oct, 24 2017";
      container.SetMetadata();
    }

    static void ListMetadata(CloudBlobContainer container)
    {
      container.FetchAttributes();
      foreach (var item in container.Metadata)
      {
        Console.WriteLine($"Key : {item.Key}");
        Console.WriteLine($"Value : {item.Value}\n\n");
      }
    }

  }
}
