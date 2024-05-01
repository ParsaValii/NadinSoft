using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            //TODO: add rules for other fields if needed
            _products = new Faker<Product>()
                .RuleFor(p => p.Id, (_, _) => Guid.NewGuid())
                .RuleFor(p => p.ManufactureEmail, (f, _) => f.Person.Email)
                .RuleFor(p => p.ManufacturePhone, (f, _) => f.Person.Phone)
                .RuleFor(p => p.IsAvailable, (_, _) => true)
                .RuleFor(p => p.Name, (f, _) => f.Person.FullName)
                .RuleFor(p => p.ProduceDate, (f, _) => f.Person.DateOfBirth)
                .RuleFor(p => p.UserId, (_, _) => Guid.NewGuid());


        }

        //TODO: add other fields as parameter such as User
        public static Product MockProduct(this DbContext context)
        {
            var product = _products.Generate();

            context.Set<Product>().Add(product);
            context.SaveChanges();

            return product;
        }
    }
}