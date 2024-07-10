using MediatR;
using Microsoft.AspNetCore.Identity;

namespace NadinSoft.Application.Auth.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommandRequest, RegisterCommandResponse>
{
    private readonly UserManager<IdentityUser> _userManager;

    public RegisterCommandHandler(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<RegisterCommandResponse> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
    {
        var IdentityUser = new IdentityUser
        {
            UserName = request.UserName,
            Email = request.UserName
        };
        var result = await _userManager.CreateAsync(IdentityUser, request.Password);
        return new RegisterCommandResponse(result.Succeeded);
    }
}
