using System;

namespace Prescriptions.DTO.Responses
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public Guid RefreshToken { get; set; }
    }
}