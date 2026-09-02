public interface IProductService
{
    Task<Product?> GetProduct(int id);
    Task<IEnumerable<Product>> GetProducts();
    Task<Product> CreateProduct(Product product);
    Task<Product> UpdateProduct(Product product);
    Task DeleteProduct(int id);
}