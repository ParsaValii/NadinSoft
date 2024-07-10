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

    public GenerateTokenCommandHandler(IConfiguration config)
    {
        _config = config;
    }
    public async Task<GenerateTokenCommandResponse> Handle(GenerateTokenCommandRequest request, CancellationToken cancellationToken)
    {
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,request.UserName),
                new Claim(ClaimTypes.Role,"Admin")
            };
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
