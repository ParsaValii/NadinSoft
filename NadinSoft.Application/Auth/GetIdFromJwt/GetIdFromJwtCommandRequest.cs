using MediatR;

namespace NadinSoft.Application.Auth.GetIdFromJwt;

public record GetIdFromJwtCommandRequest(string Token): IRequest<GetIdFromJwtCommandResponse>;
