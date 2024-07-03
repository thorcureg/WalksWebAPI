using System.ComponentModel.DataAnnotations;

namespace UdemyWebApi.Models.DTO
{
    public class AuthRegisterDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string [] Roles { get; set; }
    }    
    public class AuthLoginDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }    
    public class AuthLoginResponseDto
    {
        public string JwtToken { get; set; }
    }
}
