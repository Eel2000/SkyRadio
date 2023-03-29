using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SkyRadio.Application.Interfaces.Services;
using SkyRadio.Application.Models;
using SkyRadio.Application.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SkyRadio.Application.Services;

public class TokenProviderService : ITokenProviderService
{
    private readonly JWTParameters _jwtParameters;

    public TokenProviderService(JWTParameters jwtParameters)
    {
        _jwtParameters = jwtParameters;
    }

    public async Task<JwtSecurityToken?> GenerateAccessToken(IdentityUser user)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(user));

        string currentUserAddress = NetworkHelper.GetIpAddress();

        var claims = new[]
        {
            new Claim("uid", user.Id),
            new Claim("ipAddress", currentUserAddress),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtParameters.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            claims: claims,
            issuer: _jwtParameters.Issuer,
            audience: _jwtParameters.Audience,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddMinutes(_jwtParameters.DurationInMinutes));

        return jwtSecurityToken;
    }
}
