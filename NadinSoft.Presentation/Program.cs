using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NadinSoft.Infrastructure;
using NadinSoft.Infrastructure.Utilites;
using NadinSoft.Infrastructure.Extentions;
using NadinSoft.Infrastructure.MiddleWares;
using NadinSoft.Application.Interfaces;
using NadinSoft.Application.Services;
using NadinSoft.Infrastructure.Repositories;
using NadinSoft.Application.RepositoryInterfaces;
using MediatR;
using System.Reflection;
using NadinSoft.Application.Products.Queries.GetAllProducts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(GetAllProductsQueryHandler).Assembly);

builder.Services.AddDbContext<NadinDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NadinConnectinString")));

builder.Services.AddDbContext<NadinDbContext>(options => options.UseSqlite("Filename=:memory:"));


var automapperConfig = new MapperConfiguration(a =>
{
    a.AddProfile<MappingProfile>();

});
var mapper = automapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


builder.Services.AddCustomIdentity();

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseMiddleware<ErrorHandling>();

app.UseAuthorization();

app.MapControllers();

app.Run();
