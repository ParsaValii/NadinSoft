using MediatR;
using NadinSoft.Application.Auth.Login;

namespace NadinSoft.Application.Auth.Token;

public record GenerateTokenCommandRequest(string UserName, string Password) : IRequest<GenerateTokenCommandResponse>;
