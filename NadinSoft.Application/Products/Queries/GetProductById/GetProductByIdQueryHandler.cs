using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NadinSoft.Application.RepositoryInterfaces;

namespace NadinSoft.Application.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQueryRequest, GetProductByIdQueryResponse>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.Products.FirstOrDefaultAsync(p => p.Id == request.Id);
        return _mapper.Map<GetProductByIdQueryResponse>(product);
    }
}
