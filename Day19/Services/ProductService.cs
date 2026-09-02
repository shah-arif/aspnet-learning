using Microsoft.EntityFrameworkCore;

public class ProductService : IProductService
{
    private readonly AppDbContext _db;

    public ProductService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Product?> GetProduct(int id)
    {
        return await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _db.Products.ToListAsync();
    }

    public async Task<Product> CreateProduct(Product product)
    {
        _db.Products.Add(product);
        await _db.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateProduct(Product product)
    {
        var existingProduct = await _db.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
        if (existingProduct == null)
        {
            return null;
        }
        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        await _db.SaveChangesAsync();
        return existingProduct;
    }

    public async Task DeleteProduct(int id)
    {
        var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product == null)
        {
            return;
        }
        _db.Products.Remove(product);
        await _db.SaveChangesAsync();
    }
}