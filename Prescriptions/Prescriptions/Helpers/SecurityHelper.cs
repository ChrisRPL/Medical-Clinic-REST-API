using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Prescriptions.Helpers
{
    public class SecurityHelper
    {
        public static JwtSecurityToken GenerateToken(string username, string secret)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            Claim[] userClaims =
            {
                new(ClaimTypes.Name, username),
                new(ClaimTypes.Role, "user")
            };

            var token = new JwtSecurityToken(
                "http://localhost:5000",
                "http://localhost:5000",
                userClaims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
            );

            return token;
        }
    }
}