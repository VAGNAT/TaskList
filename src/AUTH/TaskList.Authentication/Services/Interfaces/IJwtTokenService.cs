using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TaskList.Authentication.Services.Interfaces
{
    public interface IJwtTokenService
    {
        JwtSecurityToken GetJwtToken(List<Claim> userClaims);
    }
}
