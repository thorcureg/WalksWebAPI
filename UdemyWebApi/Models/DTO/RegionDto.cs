using System.ComponentModel.DataAnnotations;

namespace UdemyWebApi.Models.DTO
{
    public class RegionDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
    public class CreateRegionDto
    {
        [Required]
        [MinLength(3,ErrorMessage ="Code has to be a minimum of 3 characters")]
        [MaxLength(3,ErrorMessage ="Code has to be a miximum of 3 characters")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100,ErrorMessage ="Code has to be a miximum of 3 characters")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
    public class UpdateRegionDto
    {
        [Required]
        [MinLength(3,ErrorMessage ="Code has to be a minimum of 3 characters")]
        [MaxLength(3,ErrorMessage ="Code has to be a miximum of 3 characters")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100,ErrorMessage ="Code has to be a miximum of 3 characters")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
