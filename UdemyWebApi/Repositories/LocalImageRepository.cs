using Microsoft.EntityFrameworkCore;
using UdemyWebApi.Data;
using UdemyWebApi.Models.Domain;

namespace UdemyWebApi.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly DataBContext dataBContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, 
            IHttpContextAccessor httpContextAccessor,
            DataBContext dataBContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dataBContext = dataBContext;
        }
        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images",  
                $"{image.FileName}{image.FileExtension}") ;

            //Upload Image to Local Path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            //needs to be url
            // https://localhost:1234/images/image.jpg 

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

            image.FilePath = urlFilePath ;

            //Add image to image table
            await dataBContext.Images.AddAsync(image) ;
            await dataBContext.SaveChangesAsync() ;

            return image ;
        }
    }
}
