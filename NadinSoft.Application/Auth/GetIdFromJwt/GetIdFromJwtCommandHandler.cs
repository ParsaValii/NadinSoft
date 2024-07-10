using MediatR;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace NadinSoft.Application.Auth.GetIdFromJwt;

public class GetIdFromJwtCommandHandler : IRequestHandler<GetIdFromJwtCommandRequest, GetIdFromJwtCommandResponse>
{
    private readonly UserManager<IdentityUser> _userManager;

        public GetIdFromJwtCommandHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
    public async Task<GetIdFromJwtCommandResponse> Handle(GetIdFromJwtCommandRequest request, CancellationToken cancellationToken)
    {
        var jwt = request.Token.Remove(0, 7);
        var email = ExtractUserEmailFromJWT(jwt);
        var stringUserId = await GetUserIdByEmail(email);
        var guidUserId = new Guid(stringUserId);
        return new GetIdFromJwtCommandResponse(guidUserId);

        string ExtractUserEmailFromJWT(string jwt)
        {
            string userEmail = "";

            var handler = new JwtSecurityTokenHandler();


            var token = handler.ReadJwtToken(jwt);


            var userEmailClaim = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);


            userEmail = userEmailClaim.Value;
            return userEmail;

        }
        async Task<string> GetUserIdByEmail(string email) =>
                        (await _userManager.FindByEmailAsync(email))?.Id ?? throw new Exception();
    }
}
