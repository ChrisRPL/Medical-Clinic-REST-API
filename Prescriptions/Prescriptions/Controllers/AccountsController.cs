using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Prescriptions.DTO.Requests;
using Prescriptions.Models;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Prescriptions.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/account")]
    public class AccountsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private MedicamentsDbContext _context;

        public AccountsController(IConfiguration configuration, MedicamentsDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] RegisterDto registerDto)
        {
            User user = _context.Users.FirstOrDefault(u => u.Login == registerDto.Username);

            if (user == null)
            {
                return Unauthorized();
            }
            
            string passwordHash = user.Password;
            string curHashedPassword = "";
            
            byte[] salt = Convert.FromBase64String(user.Salt);
            
            string currentHashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: registerDto.Password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
          
            if (passwordHash != currentHashedPassword)
            {
                return Unauthorized();
            }

            var refrToken = Guid.NewGuid();

            user.RefreshToken = refrToken.ToString();
            user.RefreshTokenExp = DateTime.Now.AddMinutes(10);
            _context.SaveChanges();
            
            return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(GenerateToken(user.Login)),
                    refreshToken = refrToken
                });
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public IActionResult RefreshToken([FromBody] string refreshToken)
        {
            User validTokenUser = _context.Users.FirstOrDefault(e => e.RefreshToken.Equals(refreshToken) && e.RefreshTokenExp < DateTime.Now);

            if (validTokenUser == null)
                return BadRequest("Wrong refresh token");
            
            var newRefreshToken = Guid.NewGuid();
            

            validTokenUser.RefreshToken = newRefreshToken.ToString();
            validTokenUser.RefreshTokenExp = DateTime.Now.AddMinutes(10);
            _context.SaveChanges();

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(GenerateToken(validTokenUser.Login)),
                refreshToken = newRefreshToken
            });
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] RegisterDto register)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: register.Password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            
            string saltBase64 = Convert.ToBase64String(salt);
            
            var user = new User()
            {
                Login = register.Username,
                Password = hashed,
                Salt = saltBase64,
                RefreshToken = null,
                RefreshTokenExp = null
            };
            
            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User was added successfully!");
        }
        
        private JwtSecurityToken GenerateToken(String username)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Secret"]));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            Claim[] userClaims =
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "user"),
            };
            
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "http://localhost:5000",
                audience: "http://localhost:5000",
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
            );

            return token;
        }
    }
}