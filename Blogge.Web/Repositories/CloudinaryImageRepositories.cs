
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Blogge.Web.Repositories
{
    public class CloudinaryImageRepositories : IImageRepositories
    {
        private readonly IConfiguration configuration;
        private readonly Account account;

        public CloudinaryImageRepositories(IConfiguration configuration)
        {
            this.configuration = configuration;
            account = new Account(
                configuration.GetSection("Cloudinary")["CloudName"],
                 configuration.GetSection("Cloudinary")["ApiKey"],
                  configuration.GetSection("Cloudinary")["ApiSecret"]
                );
        }
        public async Task<string> UploadAsync(IFormFile file)
        {
            var client = new Cloudinary(account);
            var UploadedParams = new AutoUploadParams()
            { 
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName

            };
            var uploadResult=await client.UploadAsync(UploadedParams);
            if(uploadResult != null && uploadResult.StatusCode==System.Net.HttpStatusCode.OK) {
                 
                return uploadResult.SecureUrl.ToString();
            }
            return null;
        }
    }
}
