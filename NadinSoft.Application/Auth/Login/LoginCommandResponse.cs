namespace NadinSoft.Application.Auth.Login;

public record LoginCommandResponse(bool Success, IList<string> Roles);
