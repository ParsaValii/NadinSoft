using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace NadinSoft.Application.Auth.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommandRequest, LoginCommandResponse>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _config;

    public LoginCommandHandler(UserManager<IdentityUser> userManager, IConfiguration config)
    {
        _userManager = userManager;
        _config = config;
    }

    public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
    {
        var identityUser = await _userManager.FindByEmailAsync(request.UserName);
            if (identityUser is null)
                return new LoginCommandResponse(false);

            return new LoginCommandResponse(await _userManager.CheckPasswordAsync(identityUser, request.Password));
    }
}
