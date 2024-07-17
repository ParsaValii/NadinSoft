using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NadinSoft.Application.Auth.Login;
using NadinSoft.Application.Auth.Register;
using NadinSoft.Application.Auth.Token;
using NadinSoft.Domain.Dtos;

namespace NadinSoft.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
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
        [AllowAnonymous]
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