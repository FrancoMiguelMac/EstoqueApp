namespace Estoque.Crosscutting.Dtos
{
    public class PagedProductResponse
    {
        public List<ProductDTO> Data { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }

        public PagedProductResponse(List<ProductDTO> data, int pageNumber, int pageSize, int totalRecords)
        {
            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalRecords = totalRecords;
        }
    }
}
