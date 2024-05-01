using NadinSoft.Domain.Dtos;

namespace NadinSoft.Domain.Interfaces;

public interface IAuthenticationService
{
    Task<bool> Login(LoginUserDto user);
    Task<bool> Register(LoginUserDto user);
    string GenerateTokenString(LoginUserDto user);
    public Task<Guid> GetIdFromJwt(string Jwt);
}