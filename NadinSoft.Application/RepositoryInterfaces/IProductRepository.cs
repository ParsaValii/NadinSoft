using NadinSoft.Domain.Entities;

namespace NadinSoft.Application.RepositoryInterfaces
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        Task<Guid> CreateProduct(Product p);
        Task<bool> UpdateProduct(Product p);
        Task<bool> DeleteProduct(Product p);
    }
}