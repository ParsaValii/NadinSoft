using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NadinSoft.Infrastructure;
using NadinSoft.Domain.Dtos;
using NadinSoft.Application.Interfaces;
using NadinSoft.Application.Products.Queries.GetAllProducts;
using MediatR;

namespace NadinSoft.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMediator _mediator;

        public ProductsController(IProductService productService, NadinDbContext context, IAuthenticationService authenticationService, IMediator mediator)
        {
            _productService = productService;
            _authenticationService = authenticationService;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetAllProductsQueryResponse>>> GetProducts() =>
            Ok(await _mediator.Send(new GetAllProductsQueryRequest()));


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(Guid id) =>
            await _productService.GetProductById(id) switch
            {
                null => NotFound(),
                ProductDto product => Ok(product)
            };

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductRequestDto product)
        {
            string jwt = Request.Headers.Authorization.ToString();
            var userId = await _authenticationService.GetIdFromJwt(jwt);
            await _productService.UpdateProduct(id, product, userId);
            return NoContent();
        }

        [HttpPost]
        [Authorize]
        public async Task InsertProduct(CreateProductRequestDto product)
        {
            string jwt = Request.Headers.Authorization.ToString();
            var userId = await _authenticationService.GetIdFromJwt(jwt);
            await _productService.InsertItem(product, userId);
        }



        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            string jwt = Request.Headers.Authorization.ToString();
            var userId = await _authenticationService.GetIdFromJwt(jwt);

            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productService.DeleteProduct(id, userId);
            return NoContent();
        }
    }
}