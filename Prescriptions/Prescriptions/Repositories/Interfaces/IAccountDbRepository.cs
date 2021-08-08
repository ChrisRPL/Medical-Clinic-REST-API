using System.Threading.Tasks;
using Prescriptions.DTO.Requests;
using Prescriptions.DTO.Responses;

namespace Prescriptions.Repositories.Interfaces
{
    public interface IAccountDbRepository
    {
        public Task<AuthResponseDto> Login(RegisterDto registerDto);
        public Task<AuthResponseDto> RefreshToken(string refreshToken);
        public Task Register(RegisterDto registerDto);
    }
}