using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace NadinSoft.Application.Auth.Token;

public class GenerateTokenCommandHandler : IRequestHandler<GenerateTokenCommandRequest, GenerateTokenCommandResponse>
{
    private readonly IConfiguration _config;
    private readonly UserManager<IdentityUser> _userManager;

    public GenerateTokenCommandHandler(IConfiguration config, UserManager<IdentityUser> userManager)
    {
        _config = config;
        _userManager = userManager;
    }

    public async Task<GenerateTokenCommandResponse> Handle(GenerateTokenCommandRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.UserName);
        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, request.UserName)
            };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));
        var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var securityToken = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            issuer: _config.GetSection("Jwt:issuer").Value,
            audience: _config.GetSection("Jwt:audience").Value,
            signingCredentials: signingCred
        );

        string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return new GenerateTokenCommandResponse(tokenString);
    }
}

