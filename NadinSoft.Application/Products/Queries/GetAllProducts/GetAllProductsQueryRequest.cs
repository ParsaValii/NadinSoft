using MediatR;

namespace NadinSoft.Application.Products.Queries.GetAllProducts;

public record GetAllProductsQueryRequest(): IRequest<List<GetAllProductsQueryResponse>>;
