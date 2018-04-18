using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace APIFacturacion.Util.GenerarPDF
{
    public class BlobModel
    {
        public BlobModel() { }

        public async Task<String> GetSASUrlAsync(string containerName)
        {
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("facturacionsunat_AzureStorageConnectionString"));
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference(containerName);

                if (await container.CreateIfNotExistsAsync())
                {
                    await container.SetPermissionsAsync(
                        new BlobContainerPermissions
                        {
                            PublicAccess = BlobContainerPublicAccessType.Off
                        }
                    );
                }

                BlobContainerPermissions containerPermissions = new BlobContainerPermissions();
                containerPermissions.SharedAccessPolicies.Add(
                    "twominutepolicy", new SharedAccessBlobPolicy()
                    {
                        SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-1),
                        SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(2),
                        Permissions = SharedAccessBlobPermissions.Write | SharedAccessBlobPermissions.Read,

                    });
                containerPermissions.PublicAccess = BlobContainerPublicAccessType.Off;
                container.SetPermissions(containerPermissions);
                string sas = container.GetSharedAccessSignature(new SharedAccessBlobPolicy() { }, "twominutepolicy");
                return sas;

            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    public class BlobUploadModel
    {
        public string RequestResult { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public long FileSizeInBytes { get; set; }
        public long FileSizeInKb { get { return (long)Math.Ceiling((double)FileSizeInBytes / 1024); } }

        public async Task<BlobUploadModel> UploadFileAsync(HttpPostedFileBase fileToUpload, String ContainerBlob)
        {
            if (fileToUpload == null || fileToUpload.ContentLength == 0)
            {
                return null;
            }
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("facturacionsunat_AzureStorageConnectionString"));

                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                CloudBlobContainer container = blobClient.GetContainerReference(ContainerBlob);

                if (await container.CreateIfNotExistsAsync())
                {
                    await container.SetPermissionsAsync(
                        new BlobContainerPermissions
                        {
                            PublicAccess = BlobContainerPublicAccessType.Blob
                        }
                    );
                }

                CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileToUpload.FileName);
                blockBlob.Properties.ContentType = fileToUpload.ContentType;

                await blockBlob.UploadFromStreamAsync(fileToUpload.InputStream);

                var blobUpload = new BlobUploadModel
                {
                    FileName = blockBlob.Name,
                    FileUrl = blockBlob.Uri.AbsoluteUri,
                    FileSizeInBytes = blockBlob.Properties.Length
                };

                return blobUpload;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }

    public class BlobDownloadModel
    {
        public MemoryStream BlobStream { get; set; }
        public string BlobFileName { get; set; }
        public string BlobContentType { get; set; }
        public string BlobFileUrl { get; set; }
        public long BlobLength { get; set; }

        public async Task<BlobDownloadModel> DownloadFileAsync(String ContainerBlob, String BlobReference)
        {
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("facturacionsunat_AzureStorageConnectionString"));

                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                CloudBlobContainer container = blobClient.GetContainerReference(ContainerBlob);

                CloudBlockBlob blockBlob = container.GetBlockBlobReference(BlobReference);

                MemoryStream memStream = new MemoryStream();
                await blockBlob.DownloadToStreamAsync(memStream);

                var lastPos = blockBlob.Name.LastIndexOf('/');
                var fileName = blockBlob.Name.Substring(lastPos + 1, blockBlob.Name.Length - lastPos - 1);

                var download = new BlobDownloadModel
                {
                    BlobStream = memStream,
                    BlobFileName = fileName,
                    BlobFileUrl = blockBlob.Uri.AbsoluteUri,
                    BlobLength = blockBlob.Properties.Length,
                    BlobContentType = blockBlob.Properties.ContentType
                };

                return download;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}