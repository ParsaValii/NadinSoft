using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NadinSoft.Application.RepositoryInterfaces;

namespace NadinSoft.Application.Products.Queries.GetAllProducts;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, List<GetAllProductsQueryResponse>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }
    public async Task<List<GetAllProductsQueryResponse>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
    {
        var products = _productRepository.Products;
        var result = _mapper.Map<List<GetAllProductsQueryResponse>>(await products.ToListAsync());
        return result;
    }
}
