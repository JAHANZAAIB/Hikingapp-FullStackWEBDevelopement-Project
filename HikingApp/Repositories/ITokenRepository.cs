using Microsoft.AspNetCore.Identity;

namespace HikingApp.Repositories
{
    public interface ITokenRepository
    {
        public string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
