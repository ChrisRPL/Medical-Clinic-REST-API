using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prescriptions.DTO.Requests;
using Prescriptions.Repositories.Interfaces;

namespace Prescriptions.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/account")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountDbRepository _accountDbRepository;

        public AccountsController(IAccountDbRepository accountDbRepository)
        {
            _accountDbRepository = accountDbRepository;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] RegisterDto registerDto)
        {
            try
            {
                var result = await _accountDbRepository.Login(registerDto);
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            try
            {
                var result = await _accountDbRepository.RefreshToken(refreshToken);
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto register)
        {
            await _accountDbRepository.Register(register);
            return Ok("Register done successfully");
        }
    }
}