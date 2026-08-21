public class PaginatedProductsResponse
{
    public List<Product> Products { get; set; } = new();
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalProducts { get; set; }
}