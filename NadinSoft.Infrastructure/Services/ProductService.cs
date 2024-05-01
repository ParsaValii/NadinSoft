using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NadinSoft.Domain.Dtos;
using NadinSoft.Domain.Entities;
using NadinSoft.Domain.Exeptions;
using NadinSoft.Domain.Interfaces;
using NadinSoft.Domain.Repository;

namespace NadinSoft.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly NadinDbContext _context;
        private readonly IProductRepository _productRepository;

        public ProductService(IMapper mapper, NadinDbContext context, IProductRepository productRepository)
        {
            _mapper = mapper;
            _context = context;
            _productRepository = productRepository;
        }
        public async void Dispose()
        {
            await _context.DisposeAsync();
        }

        public async Task<IEnumerable<ProductDto>> GetProducts(GetProductsRequestDto request)
        {
            var products = _productRepository.Products;
            if (request.UserId is not null)
            {
                products = products.Where(p => p.UserId == request.UserId);
                var result = _mapper.Map<IEnumerable<ProductDto>>(products).ToList();
                return result;
            }
            else
            {
                var allProducts = await _context.Products.ToListAsync();
                var result = _mapper.Map<IEnumerable<ProductDto>>(products).ToList();
                return result;
            }
        }

        public async Task<ProductDto> GetProductById(Guid id)
        {
            var products = _productRepository.Products;
            var product = await products.FirstOrDefaultAsync(p => p.Id == id);
            var result = _mapper.Map<ProductDto>(product);
            return result;
        }

        public async Task InsertItem(CreateProductRequestDto product, Guid userId)
        {
            var prod = _mapper.Map<Product>(product);
            prod.UserId = userId;
            await _productRepository.CreateProduct(prod);
        }

        public async Task UpdateProduct(Guid id, UpdateProductRequestDto product, Guid userId)
        {
            var products = _productRepository.Products;
            var prodFromDb = await products.FirstOrDefaultAsync(p => p.Id == id);
            if (prodFromDb is not null && prodFromDb.UserId == userId)
            {
                _mapper.Map(product, prodFromDb);
                _context.SaveChanges();
            }
            else if (prodFromDb is not null && prodFromDb.UserId != userId)
            {
                throw new AccessDeniedException("Access Denied");
            }
            else
            {
                throw new EntityNotFoundException("Product Not Found");
            }
        }

        public async Task DeleteProduct(Guid id, Guid userId)
        {
            var products = _productRepository.Products;
            Product? prodFromDb = await products.FirstOrDefaultAsync(p => p.Id == id);
            if (prodFromDb == null)
            {
                throw new EntityNotFoundException("Product Not Found");
            }
            else if (prodFromDb.UserId == userId && prodFromDb is not null)
            {
                await _productRepository.DeleteProduct(prodFromDb);
            }
            else if (prodFromDb is not null && prodFromDb.UserId != userId)
            {
                throw new AccessDeniedException("Access Denied");
            }
        }
    }
}