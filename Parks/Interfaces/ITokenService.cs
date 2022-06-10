using Park.Models;

namespace Park.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);
    }
}