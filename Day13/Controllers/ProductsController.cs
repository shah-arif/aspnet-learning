using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("filter")] // curl -s 'http://localhost:5156/api/products/filter?maxPrice=100&minPrice=50' | jq    
    public async Task<IActionResult> GetProductsFiltered(decimal? maxPrice, decimal? minPrice)
    {
        var products = await _context.Products.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToListAsync();
        return Ok(products);
    }

    [HttpGet] // curl -s 'http://localhost:5156/api/products' | jq
    public async Task<IActionResult> GetProducts()
    {
        var products = await _context.Products.ToListAsync();
        return Ok(products);
    }

    [HttpPost] // curl -s -X POST 'http://localhost:5156/api/products' -d '{"name":"test 3","price":120,"stock":6}' -H "Content-Type: application/json" | jq
    public async Task<IActionResult> CreateProduct(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return Ok(product);
    }

    [HttpGet("{id}")] // curl -s 'http://localhost:5156/api/products/1' | jq
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound(new { message = $"Product with id {id} not found." });
        }

        return Ok(product);
    }

    [HttpPut("{id}")] // curl -s -X PUT 'http://localhost:5156/api/products/1' -d '{"name":"test 3","price":120,"stock":6}' -H "Content-Type: application/json" | jq
    public async Task<IActionResult> UpdateProduct(int id, Product product)
    {
        var existingProduct = await _context.Products.FindAsync(id);

        if (existingProduct == null)
        {
            return NotFound(new { message = $"Product with id {id} not found." });
        }

        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.Stock = product.Stock;

        await _context.SaveChangesAsync();

        return Ok(existingProduct);
    }
}