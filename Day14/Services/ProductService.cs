using Microsoft.EntityFrameworkCore;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProductResponseDto>> GetAllAsync()
    {
        return await _context.Products
            .AsNoTracking()
            .Select(p => new ProductResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                Category = p.Category

            })
            .ToListAsync();
            
    }

    public async Task<ProductResponseDto?> GetByIdAsync(int id)
    {
        return await _context.Products
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new ProductResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                Category = p.Category
            })
            .FirstOrDefaultAsync();
    }

    public async Task<ProductResponseDto> CreateAsync(ProductCreateDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            Stock = dto.Stock,
            Category = dto.Category
        };

        _context.Products.Add(product);
        
        await _context.SaveChangesAsync();

        return new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Stock = product.Stock,
            Category = product.Category
        };
    }

    public async Task<bool> UpdateAsync(int id, ProductUpdateDto dto)
    {
        var product = await _context.Products.FindAsync(id);

        if (product is null)
        {
            return false;
        }

        product.Name = dto.Name;
        product.Price = dto.Price;
        product.Stock = dto.Stock;
        product.Category = dto.Category;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product is null)
        {
            return false;
        }

        _context.Products.Remove(product);

        await _context.SaveChangesAsync();

        return true;
    }
}