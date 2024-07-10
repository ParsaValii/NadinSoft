using MediatR;

namespace NadinSoft.Application.Products.Commands.UpdateProduct;

public record UpdateProductCommandRequest(Guid Id, string Name, string ManufacturePhone, string ManufactureEmail, bool IsAvailable, Guid UserId) : IRequest<UpdateProductCommandResponse>;
