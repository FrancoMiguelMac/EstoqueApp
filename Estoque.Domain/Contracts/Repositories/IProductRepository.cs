using Estoque.Crosscutting.Dtos;
using Estoque.Domain.Entities;

namespace Estoque.Domain.Contracts.Repositories
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<Product?> GetProductByName(string name);
        Task<PagedProductResponse> GetPaginatedAndFilteredProducts(PaginationParamsDTO paginationParams, decimal conversion);
    }
}
