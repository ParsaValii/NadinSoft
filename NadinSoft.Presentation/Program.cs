using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NadinSoft.Infrastructure;
using NadinSoft.Infrastructure.Extentions;
using NadinSoft.Infrastructure.MiddleWares;
using NadinSoft.Infrastructure.Repositories;
using NadinSoft.Application.RepositoryInterfaces;
using MediatR;
using System.Reflection;
using NadinSoft.Application.Products.Queries.GetAllProducts;
using NadinSoft.Application.Mapping;
using NadinSoft.Presentation.ExtentionMethods;
using NSwag;
using NSwag.Generation.Processors.Security;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApiDocument(configure =>
{
    configure.Title = "Your API";
    configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
    {
        Type = OpenApiSecuritySchemeType.ApiKey,
        Name = "Authorization",
        In = OpenApiSecurityApiKeyLocation.Header,
        Description = "Type into the textbox: Bearer {your JWT token}."
    });

    configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
});

builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(GetAllProductsQueryHandler).Assembly);

builder.Services.AddDbContext<NadinDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NadinConnectionString")));

builder.Services.AddDbContext<NadinDbContext>(options => options.UseSqlite("Filename=:memory:"));


var automapperConfig = new MapperConfiguration(a =>
{
    a.AddProfile<MappingProfile>();

});
var mapper = automapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


builder.Services.AddCustomIdentity();

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseMiddleware<ErrorHandling>();

app.UseAuthorization();

app.MapControllers();

DatabaseInitializer.Initialize(app.Services);


app.Run();
