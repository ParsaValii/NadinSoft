using AutoMapper;
using NadinSoft.Application.Products.Commands;
using NadinSoft.Application.Products.Queries.GetAllProducts;
using NadinSoft.Application.Products.Queries.GetProductById;
using NadinSoft.Domain.Dtos;
using NadinSoft.Domain.Entities;

namespace NadinSoft.Infrastructure.Utilites
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, CreateProductRequestDto>().ReverseMap();
            CreateMap<Product, UpdateProductRequestDto>().ReverseMap();
            CreateMap<Product, GetAllProductsQueryResponse>().ReverseMap();
            CreateMap<GetProductByIdQueryResponse, Product>().ReverseMap();
            CreateMap<CreateProductCommandRequest, Product>().ReverseMap();
        }
    }
}