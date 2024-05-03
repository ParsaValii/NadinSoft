using NadinSoft.Application.RepositoryInterfaces;
using NadinSoft.Domain.Entities;

namespace NadinSoft.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private NadinDbContext _context;
        public ProductRepository(NadinDbContext context)
        {
            _context = context;
        }
        public IQueryable<Product> Products => _context.Products;
        public async Task CreateProduct(Product p)
        {
            await _context.AddAsync(p);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteProduct(Product p)
        {
            _context.Remove(p);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProduct(Product p)
        {
            _context.Update(p);
            await _context.SaveChangesAsync();
        }
    }
}