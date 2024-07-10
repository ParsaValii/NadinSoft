using MediatR;

namespace NadinSoft.Application.Auth.Register;

public record RegisterCommandRequest(string UserName, string Password): IRequest<RegisterCommandResponse>;
