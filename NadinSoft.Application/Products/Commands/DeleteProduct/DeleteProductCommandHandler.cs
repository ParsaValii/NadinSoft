using AutoMapper;
using MediatR;
using NadinSoft.Application.RepositoryInterfaces;

namespace NadinSoft.Application.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }
    
    public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
    {
        var product = _productRepository.Products.FirstOrDefault(p => p.Id == request.Id);
        if (product is not null && product.UserId == request.UserId)
        {
            return new DeleteProductCommandResponse(await _productRepository.DeleteProduct(product));
        }
        return new DeleteProductCommandResponse(false);
    }
}
