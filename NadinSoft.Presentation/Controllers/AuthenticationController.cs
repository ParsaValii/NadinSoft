using MediatR;
using Microsoft.AspNetCore.Mvc;
using NadinSoft.Application.Auth.Login;
using NadinSoft.Application.Auth.Register;
using NadinSoft.Application.Auth.Token;
using NadinSoft.Application.Interfaces;
using NadinSoft.Domain.Dtos;

namespace NadinSoft.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly IMediator _mediator;

        public AuthenticationController(IAuthenticationService authService, IMediator mediator)
        {
            _authService = authService;
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterCommandRequest request)
        {
            var user = await _mediator.Send(request);
            if (user.Success == true)
            {
                return Ok(user);
            }
            return BadRequest();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginCommandRequest request)
        {
            var user = await _mediator.Send(request);
            if (user.Success == true)
            {
                var generateTokenCommandRequest = new GenerateTokenCommandRequest(UserName: request.UserName, Password: request.Password);
                var req = generateTokenCommandRequest;
                return Ok(await _mediator.Send(req));
            }
            return BadRequest();
        }
    }
}