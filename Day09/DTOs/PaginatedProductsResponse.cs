public class PaginatedProductsResponse
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalProducts { get; set; }
    public List<Product> Products { get; set; } = new();
}