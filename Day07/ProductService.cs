public class ProductService
{
    private readonly List<Product> _products;
    public ProductService(List<Product> products)
    {
        _products = products;
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        await Task.Delay(1000);
        return _products;
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        await Task.Delay(1000);
        return _products.FirstOrDefault(p => p.Id == id);
    }
}