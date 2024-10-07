using Estoque.Application.Contracts;
using Estoque.Crosscutting.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Estoque.API.Controllers
{
    [Route("[controller]")]
    public class ProductController : ApiController
    {
        private readonly IProductApplicationService _productApplicationService;
        public ProductController(IProductApplicationService productApplicationService)
        {
            _productApplicationService = productApplicationService;
        }

        [HttpPost("AddProduct")]
        public async Task<ActionResult> AddProduct([FromBody] NewProductDTO dto)
        {
            return CustomResponse(await _productApplicationService.AddProduct(dto));
        }

        [HttpPut("UpdateProduct")]
        public async Task<ActionResult> UpdateProduct([FromBody] EditProductDTO dto)
        {
            return CustomResponse(await _productApplicationService.UpdateProduct(dto));
        }

        [HttpDelete("RemoveProduct/{id}")]
        public async Task<IActionResult> RemoveProduct([FromRoute] Guid id)
        {
            return CustomResponse(await _productApplicationService.RemoveProduct(id));
        }

        [HttpGet("GetAllProducts")]
        public async Task<IEnumerable<ProductDTO>> GetAllProducts([FromQuery] string? currencyCode = null, [FromQuery] string? anotherCurrencyCode = null)
        {
            return await _productApplicationService.GetAllProducts(currencyCode, anotherCurrencyCode);
        }

        [HttpGet("GetPaginatedAndFilteredProducts")]
        public async Task<PagedProductResponse> GetPaginatedAndFilteredProducts([FromQuery] PaginationParamsDTO paginationParams)
        {
            return await _productApplicationService.GetPaginatedAndFilteredProducts(paginationParams);
        }

        [HttpGet("GetProductById/{id}")]
        public async Task<ProductDTO> GetProductById([FromRoute] Guid id, [FromQuery] string? currencyCode = null, [FromQuery] string? anotherCurrencyCode = null)
        {
            return await _productApplicationService.GetProductById(id, currencyCode, anotherCurrencyCode);
        }
    }
}
