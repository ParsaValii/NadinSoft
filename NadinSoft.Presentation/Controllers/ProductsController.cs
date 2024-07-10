using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NadinSoft.Infrastructure;
using NadinSoft.Domain.Dtos;
using NadinSoft.Application.Interfaces;
using NadinSoft.Application.Products.Queries.GetAllProducts;
using MediatR;
using NadinSoft.Application.Products.Queries.GetProductById;
using NadinSoft.Application.Products.Commands.CreateProduct;
using NadinSoft.Application.Products.Commands.UpdateProduct;
using NadinSoft.Application.Products.Commands.DeleteProduct;
using NadinSoft.Application.Auth.GetIdFromJwt;

namespace NadinSoft.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMediator _mediator;

        public ProductsController(NadinDbContext context, IAuthenticationService authenticationService, IMediator mediator)
        {
            _authenticationService = authenticationService;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetAllProductsQueryResponse>>> GetProducts() =>
            Ok(await _mediator.Send(new GetAllProductsQueryRequest()));


        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductByIdQueryResponse>> GetProduct(Guid id) =>
            Ok(await _mediator.Send(new GetProductByIdQueryRequest(id)));

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<UpdateProductCommandResponse>> UpdateProduct(UpdateProductCommandRequest request)
        {
            string jwt = Request.Headers.Authorization.ToString();
            var getIdFromJwtCommandResponse = await _mediator.Send(new GetIdFromJwtCommandRequest(jwt));
            return Ok(await _mediator.Send(request with { UserId = getIdFromJwtCommandResponse.UserId }));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CreateProductCommandResponse>> InsertProduct(CreateProductCommandRequest request)
        {
            string jwt = Request.Headers.Authorization.ToString();
            var getIdFromJwtCommandResponse = await _mediator.Send(new GetIdFromJwtCommandRequest(jwt));
            return Ok(await _mediator.Send(request with { UserId = getIdFromJwtCommandResponse.UserId }));
        }



        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<DeleteProductCommandResponse>> DeleteProduct(Guid id)
        {
            string jwt = Request.Headers.Authorization.ToString();
            var getIdFromJwtCommandResponse = await _mediator.Send(new GetIdFromJwtCommandRequest(jwt));
            
            return Ok(await _mediator.Send(new DeleteProductCommandRequest(id, getIdFromJwtCommandResponse.UserId)));
        }
    }
}