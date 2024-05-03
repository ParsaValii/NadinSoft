using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NadinSoft.Application.Interfaces;
using NadinSoft.Application.RepositoryInterfaces;
using NadinSoft.Domain.Dtos;
using NadinSoft.Domain.Entities;
using NadinSoft.Domain.Exeptions;

namespace NadinSoft.Application.Services;
public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public ProductService(IMapper mapper, IProductRepository productRepository) =>
        (_mapper, _productRepository) = (mapper, productRepository);


    public async Task<IEnumerable<ProductDto>> GetProducts(GetProductsRequestDto request)
    {
        var products = _productRepository.Products;

        if (request.UserId is not null)
            products = products.Where(p => p.UserId == request.UserId);

        return _mapper.Map<IEnumerable<ProductDto>>(await products.ToListAsync());
    }

    public async Task<ProductDto> GetProductById(Guid id)
    {
        var product = await _productRepository.Products.FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
            throw new EntityNotFoundException("Product Not Found");

        return _mapper.Map<ProductDto>(product);
    }

    public async Task InsertItem(CreateProductRequestDto request, Guid userId)
    {
        var product = _mapper.Map<Product>(request);
        product.UserId = userId;
        await _productRepository.CreateProduct(product);
    }

    public async Task UpdateProduct(Guid productId, UpdateProductRequestDto request, Guid userId)
    {
        var existingProduct = await _productRepository.Products.FirstOrDefaultAsync(p => p.Id == productId);

        if (existingProduct == null)
            throw new EntityNotFoundException("Product Not Found");

        if (existingProduct is not null && existingProduct.UserId != userId)
            throw new AccessDeniedException("Access Denied");

        _mapper.Map(existingProduct, request);
        await _productRepository.UpdateProduct(existingProduct!);
    }

    public async Task DeleteProduct(Guid id, Guid userId)
    {
        Product? prodFromDb = await _productRepository.Products.FirstOrDefaultAsync(p => p.Id == id);

        if (prodFromDb == null)
            throw new EntityNotFoundException("Product Not Found");

        if (prodFromDb is not null && prodFromDb.UserId != userId)
            throw new AccessDeniedException("Access Denied");

        await _productRepository.DeleteProduct(prodFromDb!);
    }
}