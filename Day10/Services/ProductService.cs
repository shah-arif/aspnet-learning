public class ProductService : IProductService
{
    private readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Apple", Price = 1.2m, Stock = 10, Category = "Fruit" },
        new Product { Id = 2, Name = "Orange", Price = 1.5m, Stock = 15, Category = "Fruit" },
        new Product { Id = 3, Name = "Banana", Price = 1.3m, Stock = 20, Category = "Fruit" },
        new Product { Id = 4, Name = "Mango", Price = 1.9m, Stock = 30, Category = "Fruit" },
        new Product { Id = 5, Name = "Grape", Price = 1.7m, Stock = 40, Category = "Fruit" },
        // Vegetables
        new Product { Id = 6, Name = "Carrot", Price = 0.5m, Stock = 50, Category = "Vegetable" },
        new Product { Id = 7, Name = "Tomato", Price = 0.6m, Stock = 60, Category = "Vegetable" },
        new Product { Id = 8, Name = "Cucumber", Price = 0.8m, Stock = 70, Category = "Vegetable" },
    };
    public List<Product> GetAll()
    {
        return _products;
    }

    public Product? GetById(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public Product Create(Product product)
    {
        product.Id = _products.Count == 0 ? 1 : _products.Max(p => p.Id) + 1;

        _products.Add(product);

        return product;
    }

    public bool Update(int id, Product product)
    {
        var existingProduct = GetById(id);

        if (existingProduct == null)
        {
            return false;
        }

        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.Stock = product.Stock;
        existingProduct.Category = product.Category;

        return true;
    }

    public bool Delete(int id)
    {
        var existingProduct = GetById(id);

        if (existingProduct == null)
        {
            return false;
        }

        _products.Remove(existingProduct);

        return true;
    }


}