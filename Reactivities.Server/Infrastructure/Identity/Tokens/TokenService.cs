using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Utility;
using Infrastructure.Identity.Tokens.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using User = Infrastructure.Identity.Models.User;

namespace Infrastructure.Identity.Tokens;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

    public TokenService(IConfiguration configuration)
    {
        this._configuration = configuration;
        this._jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
    }

    public string GenerateToken(User user)
    {
        var claims = new List<Claim>()
        {
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.NameIdentifier, user.ProfileId.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Sid, user.Id)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration[GlobalConstants.TokenKey]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = credentials
        };

        var token = this._jwtSecurityTokenHandler.CreateToken(tokenDescriptor);

        return this._jwtSecurityTokenHandler.WriteToken(token);
    }
}