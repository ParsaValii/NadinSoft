using NadinSoft.Domain.Entities;

namespace NadinSoft.Application.RepositoryInterfaces
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        Task CreateProduct(Product p);
        Task UpdateProduct(Product p);
        Task DeleteProduct(Product p);
    }
}