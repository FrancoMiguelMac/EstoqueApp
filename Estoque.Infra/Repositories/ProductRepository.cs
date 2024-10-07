using Estoque.Crosscutting.Dtos;
using Estoque.Domain.Contracts.Repositories;
using Estoque.Domain.Entities;
using Estoque.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Estoque.Infra.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(MicroServiceContext context) : base(context)
        {
        }

        public async Task<PagedProductResponse> GetPaginatedAndFilteredProducts(PaginationParamsDTO paginationParams, decimal conversion)
        {
            IQueryable<Product> query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(paginationParams.SearchTerm))
            {
                query = query.Where(x => x.Name.Contains(paginationParams.SearchTerm));
            }

            int totalRecords = await query.CountAsync();
            List<Product> products = await query.Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                                   .Take(paginationParams.PageSize)
                                   .ToListAsync();

            PagedProductResponse pagedProductResponse = new(
                products.Select(x => new ProductDTO(x.Id, x.Name, x.Value * conversion, x.CreatedDate, x.ModifiedDate)).ToList(),
                paginationParams.PageNumber,
                paginationParams.PageSize,
                totalRecords);

            return pagedProductResponse;
        }

        public Task<Product?> GetProductByName(string name)
        {
            return _context.Products.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
