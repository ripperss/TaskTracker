using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Application.Common.Interfaces;
using TaskTracker.Domain.Users;

namespace TaskTracker.Infastructore.Auth;

public class JwtTokenGeneration : IJwtTokenGeneration
{
    public string GenerationJwtToken(User user)
    {
        var handler = new JwtSecurityTokenHandler();



        string token = "f";


        return token;
    }


    private ClaimsIdentity GenerationClaims(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        return new ClaimsIdentity(claims);
    }
}
