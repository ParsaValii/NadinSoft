using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NadinSoft.Domain.Dtos;
using NadinSoft.Domain.Entities;

namespace NadinSoft.Domain.Repository
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        Task CreateProduct(Product p);
        Task DeleteProduct(Product p);
        Task SaveProduct(Product p);
    }
}