using MediatR;
using Microsoft.AspNetCore.Identity;

namespace NadinSoft.Application.Auth.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommandRequest, RegisterCommandResponse>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public RegisterCommandHandler(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<RegisterCommandResponse> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
    {
        // ایجاد کاربر
        var identityUser = new IdentityUser
        {
            UserName = request.UserName,
            Email = request.UserName
        };
        var result = await _userManager.CreateAsync(identityUser, request.Password);

        if (result.Succeeded)
        {
            foreach (var role in request.Roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
                await _userManager.AddToRoleAsync(identityUser, role);
            }

            return new RegisterCommandResponse(true);
        }

        return new RegisterCommandResponse(false);
    }
}
