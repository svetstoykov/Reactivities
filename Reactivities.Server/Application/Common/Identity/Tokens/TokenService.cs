using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Identity.Tokens.Interfaces;
using Domain.Common.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.Common;

namespace Application.Common.Identity.Tokens
{
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
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Email, user.Email)
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
}
