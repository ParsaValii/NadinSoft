using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NadinSoft.Domain.Dtos;
using NadinSoft.Domain.Interfaces;

namespace NadinSoft.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;

        public AuthenticationService(UserManager<IdentityUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }


        public async Task<bool> Register(LoginUserDto user)
        {
            var IdentityUser = new IdentityUser
            {
                UserName = user.UserName,
                Email = user.UserName
            };
            var result = await _userManager.CreateAsync(IdentityUser, user.Password);
            return result.Succeeded;
        }


        public async Task<bool> Login(LoginUserDto user)
        {
            var identityUser = await _userManager.FindByEmailAsync(user.UserName);
            if (identityUser is null)
                return false;

            return await _userManager.CheckPasswordAsync(identityUser, user.Password);
        }

        public async Task<Guid> GetIdFromJwt(string Jwt)
        {
            var jwt = Jwt.Remove(0, 7);
            var email = ExtractUserEmailFromJWT(jwt);
            var stringUserId = await GetUserIdByEmail(email);
            var guidUserId = new Guid(stringUserId);
            return guidUserId;

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




        public string GenerateTokenString(LoginUserDto user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.UserName),
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
            return tokenString;
        }
    }
}