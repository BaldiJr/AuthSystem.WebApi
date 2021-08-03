using AuthSystem.Repository.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuhtSystem.Business.Token
{
    public static class TokenService
    {
        public static TokenInformation GenerateToken(User user, TokenConfiguration tokenConfiguration)
        {
            try
            {
                var expires = DateTime.UtcNow.AddMinutes(tokenConfiguration.TokenExpiresMinutes);
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(tokenConfiguration.Key);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.Email.ToString()),
                        //new Claim(ClaimTypes.Role, user.IsAdmin.ToString())
                    }),
                    Expires = expires,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenWrited = tokenHandler.WriteToken(token);

                return new TokenInformation(tokenWrited, expires);
            }
            catch
            {
                return null;
            }
        }
    }
}
