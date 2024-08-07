using Microsoft.EntityFrameworkCore;
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
        public async Task<Guid> CreateProduct(Product p)
        {
            await _context.AddAsync(p);
            await _context.SaveChangesAsync();
            return p.Id;
        }
        public async Task<bool> DeleteProduct(Product p)
        {
            _context.Remove(p);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateProduct(Product p)
        {
            var existingProduct = await _context.Products.FindAsync(p.Id);
            if (existingProduct != null)
            {
                _context.Entry(existingProduct).CurrentValues.SetValues(p);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}