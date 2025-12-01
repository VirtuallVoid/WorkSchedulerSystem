using Application.DTOs.Auth.Responses;
using Application.Exceptions;
using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Settings;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IOptions<JwtSettings> _jwtSettings;

        public AuthService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public AuthResponseDto GenerateAccessToken(User user)
        {
            if (user == null)
                throw new AuthException("Invalid user provided", "INVALID_USER");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.RoleName)
            };

            var expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.Value.AccessTokenExpiryMinutes);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Value.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                issuer: _jwtSettings.Value.Issuer,
                audience: _jwtSettings.Value.Audience,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            var jwt =  new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return new AuthResponseDto
            {
                BearerToken = jwt,
                TokenExpirationDate = expiration,
                UserId = user.Id,
                UserName = user.Username,
                Role = user.RoleName
            };
        }
    }
}
