using Estoque.API.Extensions;
using Estoque.Application.Contracts;
using Estoque.Application.Implementations;
using Estoque.Crosscutting.Dtos;
using Estoque.Domain.Contracts.Repositories;
using Estoque.Domain.Contracts.Services;
using Estoque.Infra.Context;
using Estoque.Infra.ExternalServices;
using Estoque.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

QuoteApiServiceEndpoint quoteApiServiceEndpointModel = new();
builder.Configuration.GetSection("QuoteApiServiceEndpoint").Bind(quoteApiServiceEndpointModel);
builder.Services.AddSingleton(quoteApiServiceEndpointModel);

builder.Services.AddHttpClient<IQuotesApiService, QuotesApiService>(client => { client.BaseAddress = new Uri(quoteApiServiceEndpointModel.Address); });

builder.Services.AddDbContext<MicroServiceContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddStackExchangeRedisCache(options =>
    options.Configuration = builder.Configuration.GetConnectionString("Cache"));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductApplicationService, ProductApplicationService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
