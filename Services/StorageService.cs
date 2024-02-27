using System.Text.RegularExpressions;
using Azure.Storage.Blobs;

namespace Blog.Services;


public class StorageService
{
    public string Upload(string base64Image, string container)
    {
        var connectionString = Configuration.StorageConnectionString;

        var fileName = Guid.NewGuid().ToString() + ".jpg";
        var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(base64Image, "");

        byte[] imageBytes = Convert.FromBase64String(data);
        var blobClient = new BlobClient(connectionString, fileName, container);

        using var stream = new MemoryStream(imageBytes);

        blobClient.Upload(stream);

        return blobClient.Uri.AbsoluteUri;
    }
}