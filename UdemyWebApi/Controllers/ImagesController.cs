using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyWebApi.Models.Domain;
using UdemyWebApi.Models.DTO;
using UdemyWebApi.Repositories;
using static System.Net.Mime.MediaTypeNames;
using Image = UdemyWebApi.Models.Domain.Image;

namespace UdemyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        //POST: /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadDto uploadDto)
        {
            ValidateFileUpload(uploadDto);

            if (ModelState.IsValid)
            {
                var imageEntity = new Image
                {
                    File = uploadDto.File,
                    FileExtension = Path.GetExtension(uploadDto.File.FileName),
                    FileSizeInBytes = uploadDto.File.Length,
                    FileName = uploadDto.FileName,
                    FileDescription = uploadDto.FileDescription,
                };
                //Add repository to upload image
                await imageRepository.Upload(imageEntity);
            
                return Ok(imageEntity);
            }

            return BadRequest(ModelState);
        }
        private void ValidateFileUpload(ImageUploadDto uploadDto)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtensions.Contains(Path.GetExtension(uploadDto.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported File Extension");
            }

            if (uploadDto.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File Size more than 10MB, Please Upload smaller size file");
            }
        }

    }
}
