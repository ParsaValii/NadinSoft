using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NadinSoft.Infrastructure;
using NadinSoft.Domain.Interfaces;
using NadinSoft.Domain.Dtos;

namespace NadinSoft.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IAuthenticationService _authenticationService;

        public ProductsController(IProductService productService, NadinDbContext context, IAuthenticationService authenticationService)
        {
            _productService = productService;
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts([FromQuery] GetProductsRequestDto request)
        {
            return Ok(await _productService.GetProducts(request));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
        {
            var product = await _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

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