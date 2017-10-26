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
      //SetMetadata(blobContainer);
      //ListMetadata(blobContainer);
      //CopyBlob(blobContainer);
      UploadSubdirectories(blobContainer);

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

    static void CopyBlob(CloudBlobContainer container)
    {
      CloudBlockBlob blockBlob  = container.GetBlockBlobReference("test_block_blob");
      CloudBlockBlob copyBlckBlob = container.GetBlockBlobReference("copy-of-test_block_blob");
      copyBlckBlob.StartCopyAsync(blockBlob);
    }

    static void UploadSubdirectories(CloudBlobContainer container)
    {
      CloudBlobDirectory directory = container.GetDirectoryReference("parent-directory");
      CloudBlobDirectory subdirectory = directory.GetDirectoryReference("child-directory");
      CloudBlockBlob blockBlob = subdirectory.GetBlockBlobReference("532_OD_Changes.pdf");

      using (var fileStream = File.OpenRead(@"D:\temp\532_OD_Changes.pdf"))
      {
        blockBlob.UploadFromStream(fileStream);
      }
    }

  }
}
