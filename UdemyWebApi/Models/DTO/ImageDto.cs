using System.ComponentModel.DataAnnotations;

namespace UdemyWebApi.Models.DTO
{
    public class ImageDto
    {

    }    
    public class ImageUploadDto
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
    }
}
