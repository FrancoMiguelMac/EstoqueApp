using Estoque.Application.Contracts;
using Estoque.Crosscutting.Dtos;
using Estoque.Domain.Contracts.Repositories;
using Estoque.Domain.Contracts.Services;
using Estoque.Domain.Entities;
using FluentValidation.Results;
using Microsoft.Extensions.Caching.Distributed;
using Estoque.Crosscutting.Extensions;

namespace Estoque.Application.Implementations
{
    public class ProductApplicationService : ApplicationServiceBase, IProductApplicationService
    {
        private readonly IProductRepository _productRepository;
        private readonly IQuotesApiService _quotesApiService;
        private readonly IDistributedCache _cache;

        public ProductApplicationService(
            IProductRepository productRepository, 
            IQuotesApiService quotesApiService,
            IDistributedCache cache)
        {
            _productRepository = productRepository;
            _quotesApiService = quotesApiService;
            _cache = cache;
        }

        public async Task<ValidationResult> AddProduct(NewProductDTO dto)
        {
            Product? existProduct = await _productRepository.GetProductByName(dto.Name);
            if (existProduct != null)
            {
                existProduct.AddValidationError("Produto já cadastrado.", "Já existe um produto cadastrado com o mesmo nome.");
                return await Task.FromResult(existProduct.ValidationResult);
            }

            Product product = new (dto.Name, dto.Value);
            if (product.IsValid())
                await _productRepository.Add(product);

            return product.ValidationResult;
        }

        public async Task<ValidationResult> UpdateProduct(EditProductDTO dto)
        {
            Product product = await _productRepository.GetById(dto.Id);
            if (product == null)
            {
                AddValidationError("Produto não encontrado.", "Não existe nenhum produto correspondente ao id informado.");
                return ValidationResult;
            }

            product.Update(dto.Name, dto.Value);

            if (product.IsValid())
                _productRepository.Update(product);

            return product.ValidationResult;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProducts(string? currencyCode = null, string? anotherCurrencyCode = null)
        {
            decimal conversion = 1;

            if (!string.IsNullOrEmpty(currencyCode) && !string.IsNullOrEmpty(anotherCurrencyCode))
                conversion = await GetQuoteAndSetRedisCache(currencyCode, anotherCurrencyCode);

            return await Task.FromResult(_productRepository.GetAll().Select(product => new ProductDTO(
                product.Id, 
                product.Name, 
                product.Value * conversion, 
                product.CreatedDate, 
                product.ModifiedDate)));
        }

        public async Task<PagedProductResponse> GetPaginatedAndFilteredProducts(PaginationParamsDTO paginationParams)
        {
            decimal conversion = 1;

            if (!string.IsNullOrEmpty(paginationParams.CurrencyCode) && !string.IsNullOrEmpty(paginationParams.AnotherCurrencyCode))
                conversion = await GetQuoteAndSetRedisCache(paginationParams.CurrencyCode, paginationParams.AnotherCurrencyCode);

            return await _productRepository.GetPaginatedAndFilteredProducts(paginationParams, conversion);
        }

        public async Task<ProductDTO> GetProductById(Guid id, string? currencyCode = null, string? anotherCurrencyCode = null)
        {
            decimal conversion = 1;

            if (!string.IsNullOrEmpty(currencyCode) && !string.IsNullOrEmpty(anotherCurrencyCode))
                conversion = await GetQuoteAndSetRedisCache(currencyCode, anotherCurrencyCode);

            Product product = await _productRepository.GetById(id);

            return new ProductDTO(
                product.Id, 
                product.Name, 
                product.Value * conversion, 
                product.CreatedDate, 
                product.ModifiedDate);
        }

        public async Task<ValidationResult> RemoveProduct(Guid productId)
        {
            Product product = await _productRepository.GetById(productId);

            if (product != null)
            {
                _productRepository.Remove(product);
                return ValidationResult;
            }

            AddValidationError("Produto não encontrado.", "Não existe nenhum produto correspondente ao id informado.");
            return ValidationResult;
        }

        private async Task<decimal> GetQuoteAndSetRedisCache(string currencyCode, string anotherCurrencyCode)
        {
            string cacheKey = $"currency-conversion-{currencyCode}-{anotherCurrencyCode}";
            var currencyConversion = await _cache.GetStringAsync(cacheKey);

            if (string.IsNullOrEmpty(currencyConversion))
            {
                // ----- Chama a api de cotações quando o cache está expirado ------//
                currencyConversion = await _quotesApiService.GetQuoteByCurrencyCodes(currencyCode, anotherCurrencyCode);
                // -----------------------------------------------------------------//

                await _cache.SetStringAsync(cacheKey, currencyConversion, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
                });
            }

            return currencyConversion.ConvertToDecimal();
        }
    }
}
