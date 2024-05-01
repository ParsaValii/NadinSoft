using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NadinSoft.Domain.Entities;
using NadinSoft.Domain.Repository;

namespace NadinSoft.Infrastructure.Services
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
        public async Task SaveProduct(Product p)
        {
            await _context.SaveChangesAsync();
        }
    }
}