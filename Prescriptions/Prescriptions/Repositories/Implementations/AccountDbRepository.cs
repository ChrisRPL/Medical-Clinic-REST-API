using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Castle.Core.Configuration;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Prescriptions.DTO.Requests;
using Prescriptions.DTO.Responses;
using Prescriptions.Helpers;
using Prescriptions.Models;
using Prescriptions.Repositories.Interfaces;

namespace Prescriptions.Repositories.Implementations
{
    public class AccountDbRepository : IAccountDbRepository
    {
        private readonly IConfiguration _configuration;
        private readonly MedicamentsDbContext _context;

        public AccountDbRepository(IConfiguration configuration, MedicamentsDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<AuthResponseDto> Login(RegisterDto registerDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Login == registerDto.Username);

            if (user == null) throw new ArgumentException("Given data is not valid!");

            var passwordHash = user.Password;

            var salt = Convert.FromBase64String(user.Salt);

            var currentHashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                registerDto.Password,
                salt,
                KeyDerivationPrf.HMACSHA1,
                10000,
                256 / 8));

            if (passwordHash != currentHashedPassword) throw new ArgumentException("Invalid password!");

            var refrToken = Guid.NewGuid();

            user.RefreshToken = refrToken.ToString();
            user.RefreshTokenExp = DateTime.Now.AddMinutes(10);
            await _context.SaveChangesAsync();

            return new AuthResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(
                    SecurityHelper.GenerateToken(user.Login, _configuration.Attributes["Secret"])),
                RefreshToken = refrToken
            };
        }

        public async Task<AuthResponseDto> RefreshToken(string refreshToken)
        {
            var validTokenUser = _context.Users.FirstOrDefault(e =>
                e.RefreshToken.Equals(refreshToken) && e.RefreshTokenExp < DateTime.Now);

            if (validTokenUser == null)
                throw new ArgumentException("Refresh token is not valid!");

            var newRefreshToken = Guid.NewGuid();


            validTokenUser.RefreshToken = newRefreshToken.ToString();
            validTokenUser.RefreshTokenExp = DateTime.Now.AddMinutes(10);
            await _context.SaveChangesAsync();

            return new AuthResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(
                    SecurityHelper.GenerateToken(validTokenUser.Login, _configuration.Attributes["Secret"])),
                RefreshToken = newRefreshToken
            };
        }

        public async Task Register(RegisterDto register)
        {
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                register.Password,
                salt,
                KeyDerivationPrf.HMACSHA1,
                10000,
                256 / 8));

            var saltBase64 = Convert.ToBase64String(salt);

            var user = new User
            {
                Login = register.Username,
                Password = hashed,
                Salt = saltBase64,
                RefreshToken = null,
                RefreshTokenExp = null
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}