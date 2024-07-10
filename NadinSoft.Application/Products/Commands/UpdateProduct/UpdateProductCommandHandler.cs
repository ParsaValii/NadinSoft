using AutoMapper;
using MediatR;
using NadinSoft.Application.RepositoryInterfaces;
using NadinSoft.Domain.Entities;

namespace NadinSoft.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {
        var existingProduct = _productRepository.Products.FirstOrDefault(p => p.Id == request.Id);
        if (existingProduct != null && existingProduct.UserId == request.UserId)
        {
            var mappedProduct = _mapper.Map<Product>(request);
            return (new UpdateProductCommandResponse(await _productRepository.UpdateProduct(mappedProduct)));
        }
        return (new UpdateProductCommandResponse(false));
    }
}
