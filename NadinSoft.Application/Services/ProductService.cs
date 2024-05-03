using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NadinSoft.Application.RepositoryInterfaces;
using NadinSoft.Domain.Dtos;
using NadinSoft.Domain.Entities;
using NadinSoft.Domain.Exeptions;
using NadinSoft.Domain.Interfaces;

namespace NadinSoft.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductService(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }


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
            var result = _mapper.Map<ProductDto>(product);
            return result;
        }

        public async Task InsertItem(CreateProductRequestDto product, Guid userId)
        {
            var prod = _mapper.Map<Product>(product);
            prod.UserId = userId;
            await _productRepository.CreateProduct(prod);
        }
        //TODO: update from repository
        public async Task UpdateProduct(Guid id, UpdateProductRequestDto product, Guid userId)
        {
            var prodFromDb = await _productRepository.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (prodFromDb == null)
                throw new EntityNotFoundException("Product Not Found");

            if (prodFromDb is not null && prodFromDb.UserId != userId)
                throw new AccessDeniedException("Access Denied");

            _mapper.Map(prodFromDb, product);
            await _productRepository.UpdateProduct(prodFromDb!);
        }

        public async Task DeleteProduct(Guid id, Guid userId)
        {
            var products = _productRepository.Products;
            Product? prodFromDb = await products.FirstOrDefaultAsync(p => p.Id == id);

            if (prodFromDb == null)
                throw new EntityNotFoundException("Product Not Found");

            if (prodFromDb is not null && prodFromDb.UserId != userId)
                throw new AccessDeniedException("Access Denied");

            await _productRepository.DeleteProduct(prodFromDb!);
        }
    }
}