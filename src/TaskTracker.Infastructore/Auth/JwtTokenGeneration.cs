using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskTracker.Application.Common.Interfaces;
using TaskTracker.Domain.Users;

namespace TaskTracker.Infastructore.Auth;

public class JwtTokenGeneration : IJwtTokenGeneration
{
    private readonly JwtSettings _settings;

    public JwtTokenGeneration(IOptions<JwtSettings> settings)
    {
        _settings = settings.Value;
    }
    
    public async Task<string> GenerationJwtToken(User user)
    {
        var handler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII
            .GetBytes(_settings.TokenPrivateKey!);

        var expires = DateTime.UtcNow.AddMinutes(_settings.Expires);

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = await GenerationClaims(user),
            Expires = expires,
            SigningCredentials = credentials
        };

        var token = handler.CreateToken(tokenDescriptor);

        return handler.WriteToken(token);
    }


    private async Task<ClaimsIdentity> GenerationClaims(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        return new ClaimsIdentity(claims);
    }
}
