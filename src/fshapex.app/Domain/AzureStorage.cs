using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Threading.Tasks;

namespace fshapex.app.Domain
{
    public class AzureStorage
    {
        //public static void Upload()
        //{
        //	CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
        //	CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

        //	CloudBlobContainer container = blobClient.GetContainerReference("mycontainer");
        //	container.CreateIfNotExists();
        //	container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

        //	CloudBlockBlob blockBlob = container.GetBlockBlobReference("vinicius.png");

        //	BitmapImage image = new BitmapImage(new Uri("‪D:\\temp\\vinicius.png"));

        //	//FileStream fileStream = new FileStream(@"‪C:\Users\vinicius\Pictures\vinicius.png", FileMode.Open);
        //	//using (var fileStream = System.IO.File.OpenRead("‪D:\\temp\\vinicius.png"))
        //	//blockBlob.UploadFromStream(fileStream);

        //	foreach (IListBlobItem item in container.ListBlobs(null, true))
        //	{
        //		if (item.GetType() == typeof(CloudBlockBlob))
        //		{
        //			CloudBlockBlob blob = (CloudBlockBlob)item;

        //			Console.WriteLine("Block blob of length {0}: {1}", blob.Properties.Length, blob.Uri);
        //		}
        //	}
        //}

        public static async void UploadFileToStorage(Stream fileStream, string fileName)
        {
            AzureStorage azureStorage = new AzureStorage();
            await azureStorage.Upload(fileStream, fileName);
        }

        async Task<bool> Upload(Stream fileStream, string fileName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference("mycontainer");
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

            await blockBlob.UploadFromStreamAsync(fileStream);

            return await Task.FromResult(true);
        }
    }

}
