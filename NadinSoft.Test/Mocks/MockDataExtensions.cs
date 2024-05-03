using Bogus;
using Microsoft.EntityFrameworkCore;
using NadinSoft.Domain.Entities;

namespace NadinSoft.Test.Mocks
{
    public static class MockDataExtensions
    {
        private static Faker<Product> _products;

        static MockDataExtensions()
        {

            _products = new Faker<Product>()
                .RuleFor(p => p.Id, (_, _) => Guid.NewGuid())
                .RuleFor(p => p.ManufactureEmail, (f, _) => f.Person.Email)
                .RuleFor(p => p.ManufacturePhone, (f, _) => f.Person.Phone)
                .RuleFor(p => p.IsAvailable, (_, _) => true)
                .RuleFor(p => p.Name, (f, _) => f.Person.FullName)
                .RuleFor(p => p.ProduceDate, (f, _) => f.Person.DateOfBirth)
                .RuleFor(p => p.UserId, (_, _) => Guid.NewGuid());


        }


        public static Product MockProduct(this DbContext context)
        {
            var product = _products.Generate();

            context.Set<Product>().Add(product);
            context.SaveChanges();

            return product;
        }
    }
}