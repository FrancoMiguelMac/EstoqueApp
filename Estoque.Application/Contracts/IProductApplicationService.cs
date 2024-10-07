using Estoque.Crosscutting.Dtos;
using FluentValidation.Results;

namespace Estoque.Application.Contracts
{
    public interface IProductApplicationService
    {
        Task<ValidationResult> AddProduct(NewProductDTO dto);
        Task<ValidationResult> UpdateProduct(EditProductDTO dto);
        Task<ValidationResult> RemoveProduct(Guid productId);
        Task<IEnumerable<ProductDTO>> GetAllProducts(string? currencyCode = null, string? anotherCurrencyCode = null);
        Task<PagedProductResponse> GetPaginatedAndFilteredProducts(PaginationParamsDTO paginationParams);

        Task<ProductDTO> GetProductById(Guid id, string? currencyCode = null, string? anotherCurrencyCode = null);
    }
}
