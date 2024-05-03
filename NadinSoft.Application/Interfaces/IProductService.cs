using NadinSoft.Domain.Dtos;

namespace NadinSoft.Application.Interfaces;
public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProducts(GetProductsRequestDto request);
    Task<ProductDto> GetProductById(Guid id);
    Task InsertItem(CreateProductRequestDto product, Guid userId);
    Task UpdateProduct(Guid id, UpdateProductRequestDto product, Guid userId);
    Task DeleteProduct(Guid id, Guid userId);
}