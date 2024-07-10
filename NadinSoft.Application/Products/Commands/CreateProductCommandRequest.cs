using MediatR;

namespace NadinSoft.Application.Products.Commands;

public record CreateProductCommandRequest(string Name, string ManufacturePhone, string ManufactureEmail, bool IsAvailable, Guid? UserId = null) : IRequest<CreateProductCommandResponse>;
