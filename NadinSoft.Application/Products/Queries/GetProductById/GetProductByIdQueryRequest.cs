using MediatR;
using NadinSoft.Application.Products.Queries.GetAllProducts;

namespace NadinSoft.Application.Products.Queries.GetProductById;

public record GetProductByIdQueryRequest(Guid Id) : IRequest<GetProductByIdQueryResponse>;
