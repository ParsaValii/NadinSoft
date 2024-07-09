using AutoMapper;
using NadinSoft.Application.Products.Queries.GetAllProducts;
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
        }
    }
}