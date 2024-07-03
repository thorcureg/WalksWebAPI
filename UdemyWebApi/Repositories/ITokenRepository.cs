using Microsoft.AspNetCore.Identity;

namespace UdemyWebApi.Repositories
{
    public interface ITokenRepository
    {
        string GetJWTToken(IdentityUser user, List<string> roles);
    }
}
