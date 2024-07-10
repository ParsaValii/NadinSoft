using MediatR;

namespace NadinSoft.Application.Products.Commands.DeleteProduct;

public record DeleteProductCommandRequest(Guid Id, Guid UserId) : IRequest<DeleteProductCommandResponse>;
