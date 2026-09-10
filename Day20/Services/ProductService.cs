using Microsoft.EntityFrameworkCore;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<ProductResponseDto>> GetAllAsync(ProductQueryDto request)
    {
        var query = _context.Products
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            query = query.Where(p => p.Name.ToLower().Contains(request.Search.ToLower()));
            // query = query.Where(p => EF.Functions.ILike(p.Name, $"%{request.Search}%"));
        }

        if (request.CategoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == request.CategoryId);
        }

        if (request.MinPrice.HasValue)
        {
            query = query.Where(p => p.Price >= request.MinPrice);
        }

        if (request.MaxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= request.MaxPrice);
        }

        if (request.InStock == true)
        {
            query = query.Where(p => p.Stock > 0);
        }

        query = request.SortBy.ToLower()
            switch
            {
                "price" => request.Descending
                    ? query.OrderByDescending(p => p.Price)
                    : query.OrderBy(p => p.Price),

                "stock" => request.Descending
                    ? query.OrderByDescending(p => p.Stock)
                    : query.OrderBy(p => p.Stock),

                _ => request.Descending
                    ? query.OrderByDescending(p => p.Name)
                    : query.OrderBy(p => p.Name)
            };

        request.Page = Math.Max(1, request.Page);
        request.PageSize = Math.Clamp(request.PageSize, 1, 100);

        var totalItems = await query.CountAsync();

        var totalPages = (int)Math.Ceiling((double)totalItems / request.PageSize);

        var skip = (request.Page - 1) * request.PageSize;

        var items = await query
            .Skip(skip)
            .Take(request.PageSize)
            .Select(p => new ProductResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name
            })
            .ToListAsync();

        return new PagedResult<ProductResponseDto>
        {
            Items = items,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalItems = totalItems,
            TotalPages = totalPages
        };
    }

    public async Task<ProductResponseDto?> GetByIdAsync(int id)
    {
        var product =await _context.Products
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new ProductResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name
            })
            .FirstOrDefaultAsync();

        if (product is null)
        {
            throw new NotFoundException("Product not found");
        }

        return product;
    }

    public async Task<ProductResponseDto> CreateAsync(ProductCreateDto dto)
    {
        var categoryExists = await _context.Categories.AnyAsync(c => c.Id == dto.CategoryId);

        if (!categoryExists)
        {
            throw new NotFoundException("Category does not exist");
        }

        // Optional conflict check
        var productExists = await _context.Products.AnyAsync(p => p.Name.ToLower() == dto.Name.ToLower());

        if (productExists)
        {
            throw new ConflictException("Product already exists");
        }

        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            Stock = dto.Stock,
            CategoryId = dto.CategoryId!.Value
        };

        _context.Products.Add(product);

        await _context.SaveChangesAsync();

        return new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            CategoryId = product.CategoryId
        };
    }

    public async Task<bool> UpdateAsync(int id, ProductUpdateDto dto)
    {
        var product = await _context.Products.FindAsync(id);

        if (product is null)
        {
            throw new NotFoundException("Product not found");
        }

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;
        product.Stock = dto.Stock;
        product.CategoryId = dto.CategoryId;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product is null)
        {
            throw new NotFoundException("Product not found");
        }

        _context.Products.Remove(product);

        await _context.SaveChangesAsync();

        return true;
    }
}