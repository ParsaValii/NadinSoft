using MediatR;

namespace NadinSoft.Application.Auth.Register;

public record RegisterCommandRequest(string UserName, string Password, List<string> Roles) : IRequest<RegisterCommandResponse>;
