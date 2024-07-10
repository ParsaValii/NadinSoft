using MediatR;

namespace NadinSoft.Application.Auth.Login;

public record LoginCommandRequest(string UserName, string Password) : IRequest<LoginCommandResponse>;
