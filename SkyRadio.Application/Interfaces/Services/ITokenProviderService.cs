using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace SkyRadio.Application.Interfaces.Services;

public interface ITokenProviderService
{
    /// <summary>
    /// Generate the beare token for jwt auth base schema.
    /// </summary>
    /// <param name="user"></param>
    /// <returns><see cref="JwtSecurityToken"/></returns>
    Task<JwtSecurityToken?> GenerateAccessToken(IdentityUser user);
}
