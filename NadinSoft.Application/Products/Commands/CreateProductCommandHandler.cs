using AutoMapper;
using MediatR;
using NadinSoft.Application.RepositoryInterfaces;
using NadinSoft.Domain.Entities;

namespace NadinSoft.Application.Products.Commands;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request);
        var productId = await _productRepository.CreateProduct(product);
        return new CreateProductCommandResponse(productId);
    }
}
