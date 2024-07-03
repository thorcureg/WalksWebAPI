using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UdemyWebApi.Models.DTO;
using UdemyWebApi.Repositories;

namespace UdemyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }
        //POST /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] AuthRegisterDto registerDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Username
            };
            var identityResult = await userManager.CreateAsync(identityUser, registerDto.Password);

            if (identityResult.Succeeded)
            {
                if (registerDto.Roles != null && registerDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerDto.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User Registerd Successfully");
                    }
                }
            }
            return BadRequest("Something went wrong");
        }

        //POST /api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] AuthLoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Username);

            if (user != null)
            {
                var checkPassword =await userManager.CheckPasswordAsync(user, loginDto.Password);

                if (checkPassword)
                {
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        //Create token
                        var jwtToken  = tokenRepository.GetJWTToken(user, roles.ToList());

                        var response = new AuthLoginResponseDto()
                        {
                            JwtToken = jwtToken,
                        };
                        return Ok(response);

                    }
                    
                }
            }

            return BadRequest("Username or password incorrect");
        }
    }
}
