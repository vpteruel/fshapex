using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Windows.Media.Imaging;

namespace fshapex.app.Domain
{
    public class PipeProcess
    {
        public long DPI { get; } = 96;

        public BitmapImage Resize(string fileName, long dpi = 96)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileName);

            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.None;
            bitmap.UriSource = new Uri(fileInfo.FullName);
            bitmap.EndInit();

            double imageDpi = (long)Math.Round((bitmap.DpiX + bitmap.DpiY) / 2d);

            OpenCvSharp.Mat mat = new OpenCvSharp.Mat(fileInfo.FullName, OpenCvSharp.ImreadModes.AnyColor);

            if (imageDpi != dpi)
            {
                double resizeFactor = dpi / imageDpi; // TODO: Usar double no lugar do long
                mat.Resize(new OpenCvSharp.Size(bitmap.Width * resizeFactor, bitmap.Height * resizeFactor));
                //mat.Resize(resizeFactor);
            }

            var directory = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "fse");
            if (!System.IO.Directory.Exists(directory))
                System.IO.Directory.CreateDirectory(directory);
            //var tempFile = Path.Combine(directory, Guid.NewGuid().ToString() + extension);
            var tempFile = System.IO.Path.Combine(directory, fileInfo.Name);

            mat.SaveImage(tempFile);

            bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.None;
            bitmap.UriSource = new Uri(tempFile);
            bitmap.EndInit();

            return bitmap;
        }

        public Uri GenerateURI(string fileName, string blobName, string container = "mycontainer", bool checkIfExists = true)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileName);
            container = container.ToLower().Replace(".", "").Replace("-", "").Replace("(", "").Replace(")", "").Replace("+", "").Replace(" ", "");

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer _container = blobClient.GetContainerReference(container);
            //if (!_container.Exists())
            //{
            _container.CreateIfNotExists();
            _container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            //}
            CloudBlockBlob blockBlob = _container.GetBlockBlobReference(blobName);
            if (checkIfExists && blockBlob.Exists())
                return blockBlob.Uri;
            using (var fileStream = System.IO.File.OpenRead(fileInfo.FullName))
                blockBlob.UploadFromStream(fileStream);
            return blockBlob.Uri;
        }

        public string RemainingTime(long milliseconds)
        {
            var _seconds = milliseconds / 1000;
            var seconds = (_seconds > 60 ? _seconds % 60 : _seconds);

            var _minutes = (_seconds > 60 ? _seconds / 60 : 0);
            var minutes = (_minutes > 60 ? _minutes % 60 : _minutes);

            var hours = (_minutes > 60 ? _minutes / 60 : 0);

            return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
        }

        public double EuclideanDistance(double x1, double x2, double y1, double y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }

        public double ManhattanDistance(double x1, double x2, double y1, double y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

        public double ChebyshevDistance(double x1, double x2, double y1, double y2)
        {
            return Math.Max(Math.Abs(x1 - x2), Math.Abs(y1 - y2));
        }
    }

    public static class Extensions
    {
        public static bool In<T>(this T obj, params T[] values)
        {
            if (obj == null || values == null || values.Length == 0)
                return false;
            foreach (var v in values)
                if (obj.Equals(v))
                    return true;
            return false;
        }
    }

}
