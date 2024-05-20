using Microsoft.AspNetCore.Mvc;
using NadinSoft.Application.Interfaces;
using NadinSoft.Domain.Dtos;

namespace NadinSoft.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public AuthenticationController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(LoginUserDto user) =>
            await _authService.Register(user) ? Ok("Successfully done") : BadRequest();

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserDto user) =>
            await _authService.Login(user) ? Ok(_authService.GenerateTokenString(user)) : BadRequest();
    }
}