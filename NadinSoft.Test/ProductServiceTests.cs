using AutoMapper;
using NadinSoft.Application.RepositoryInterfaces;
using NadinSoft.Application.Services;
using NadinSoft.Domain.Dtos;
using NadinSoft.Domain.Exeptions;
using NadinSoft.Domain.Interfaces;
using NadinSoft.Infrastructure;
using NadinSoft.Infrastructure.Repositories;
using NadinSoft.Test.Mocks;

namespace NadinSoft.Test
{
    public class ProductServiceTests
    {
        IProductService _productService;
        private NadinDbContext _context;
        private IMapper _mapper;
        private IProductRepository _repository;

        public ProductServiceTests()
        {
            _context = DatabaseFactory.CreateNadinDb();
            _mapper = AutoMapperFactory.CreateMapper();
            _repository = new ProductRepository(_context);
            _productService = new ProductService(_mapper, _repository);
        }

        [Fact]
        public async void GetProducts_WithNoFiltration_ReturnsAllProducts()
        {
            var product = _context.MockProduct();
            _context.MockProduct();

            var request = new GetProductsRequestDto(null);

            var result = await _productService.GetProducts(request);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async void GetProducts_WithFiltrationOnUser_ReturnsProductsOfTheSpecificUser()
        {
            var product1 = _context.MockProduct();
            var product2 = _context.MockProduct();

            var request = new GetProductsRequestDto(product1.UserId);

            var result = await _productService.GetProducts(request);

            Assert.Equal(product1.Id, result.Single().Id);
        }


        [Fact]
        public async void GetProductById_CheckAllFields()
        {
            var desiredProduct = _context.MockProduct();
            var otherProduct = _context.MockProduct();

            var result = await _productService.GetProductById(desiredProduct.Id);

            Assert.Equal(desiredProduct.Id, result.Id);
        }

        [Fact]
        public async void InsertProduct_Successful()
        {
            var product = new CreateProductRequestDto("New Product", DateTime.Now, "123456789", "test@example.com",
                true);

            await _productService.InsertItem(product, Guid.NewGuid());

            var dbProducts = _context.Products.ToList();

            Assert.Single(dbProducts);
        }

        [Fact]
        public async void UpdateProduct_WhenCreatorOfTheProductIsTheSameWithRequester_Successful()
        {
            var product = _context.MockProduct();
            var userId = product.UserId;

            var request = new UpdateProductRequestDto("New Product", DateTime.Now, "123456789", "test@example.com",
                true);

            await _productService.UpdateProduct(product.Id, request, userId);

            var dbProduct = _context.Products.Single();

            Assert.Equal(request.Name, dbProduct.Name);
            Assert.Equal(request.IsAvailable, dbProduct.IsAvailable);
            Assert.Equal(request.ManufactureEmail, dbProduct.ManufactureEmail);
            Assert.Equal(request.ManufacturePhone, dbProduct.ManufacturePhone);
            Assert.Equal(request.ProduceDate, dbProduct.ProduceDate);
        }

        [Fact]
        public async void UpdateProduct_WhenCreatorOfTheProductIsNotTheSameWithRequester_ThrowsAccessDeniedException()
        {
            var product = _context.MockProduct();
            var otherUserId = Guid.NewGuid();

            var request = new UpdateProductRequestDto("New Product", DateTime.Now, "123456789", "test@example.com",
                true);

            await Assert.ThrowsAsync<AccessDeniedException>(async () => await _productService.UpdateProduct(product.Id, request, otherUserId));

            var dbProduct = _context.Products.Single();

            Assert.Equal(product.Name, dbProduct.Name);
            Assert.Equal(product.IsAvailable, dbProduct.IsAvailable);
            Assert.Equal(product.ManufactureEmail, dbProduct.ManufactureEmail);
            Assert.Equal(product.ManufacturePhone, dbProduct.ManufacturePhone);
            Assert.Equal(product.ProduceDate, dbProduct.ProduceDate);
        }

        [Fact]
        public async void UpdateProduct_WhenNoProductWithRequestedIdIsFound_ThrowsEntityNotFoundException()
        {
            var product = _context.MockProduct();
            var wrongId = Guid.NewGuid();

            var request = new UpdateProductRequestDto("New Product", DateTime.Now, "123456789", "test@example.com",
                true);

            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await _productService.UpdateProduct(wrongId, request, product.UserId));

            var dbProduct = _context.Products.Single();

            Assert.Equal(product.Name, dbProduct.Name);
            Assert.Equal(product.IsAvailable, dbProduct.IsAvailable);
            Assert.Equal(product.ManufactureEmail, dbProduct.ManufactureEmail);
            Assert.Equal(product.ManufacturePhone, dbProduct.ManufacturePhone);
            Assert.Equal(product.ProduceDate, dbProduct.ProduceDate);
        }

        [Fact]
        public async void DeleteProduct_WhenCreatorOfTheProductIsTheSameWithRequester_Successful()
        {
            var product = _context.MockProduct();
            var userId = product.UserId;

            await _productService.DeleteProduct(product.Id, userId);

            var dbProduct = _context.Products.ToList();

            Assert.Empty(dbProduct);
        }


        [Fact]
        public async void DeleteProduct_WhenCreatorOfTheProductIsNotTheSameWithRequester_ThrowsAccessDeniedException()
        {
            var product = _context.MockProduct();
            var wrongUserId = Guid.NewGuid();

            await Assert.ThrowsAsync<AccessDeniedException>(async () => await _productService.DeleteProduct(product.Id, wrongUserId));

            var dbProducts = _context.Products.ToList();

            Assert.NotEmpty(dbProducts);
        }

        [Fact]
        public async void DeleteProduct_WhenNoProductWithRequestedIdIsFound_ThrowsEntityNotFoundException()
        {
            var product = _context.MockProduct();
            var wrongProductId = new Guid();

            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await _productService.DeleteProduct(wrongProductId, product.UserId));

            var dbProducts = _context.Products.ToList();

            Assert.NotEmpty(dbProducts);
        }
    }
}