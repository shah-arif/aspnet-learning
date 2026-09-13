
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[ApiController]
[Route("api/[controller]")]
public sealed class ProductsController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public ProductsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // --------------------------------------------------
    // GET: api/products
    // --------------------------------------------------

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<ProductResponse>>> GetProducts()
    {
        var products = await _dbContext.Products
            .AsNoTracking()
            .Select(x => new ProductResponse
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Stock = x.Stock
            })
            .ToListAsync();

        return Ok(products);
    }

    // --------------------------------------------------
    // GET: api/products/{id}
    // --------------------------------------------------

    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<ActionResult<ProductResponse>> GetProduct(
        int id)
    {
        var product = await _dbContext.Products
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new ProductResponse
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Stock = x.Stock
            })
            .FirstOrDefaultAsync();

        if (product is null)
        {
            return NotFound(new
            {
                message = "Product not found."
            });
        }

        return Ok(product);
    }

    // --------------------------------------------------
    // DELETE: api/products/{id}
    // --------------------------------------------------

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _dbContext.Products
            .FirstOrDefaultAsync(x => x.Id == id);

        if (product is null)
        {
            return NotFound(new
            {
                message = "Product not found."
            });
        }

        _dbContext.Products.Remove(product);

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    // --------------------------------------------------
    // DELETE: api/products/policy/{id}
    // --------------------------------------------------

    [HttpDelete("policy/{id:int}")]
    [Authorize(Policy = "CanDeleteProduct")]
    public async Task<IActionResult> DeleteProductUsingPolicy(
        int id)
    {
        var product = await _dbContext.Products
            .FirstOrDefaultAsync(x => x.Id == id);

        if (product is null)
        {
            return NotFound(new
            {
                message = "Product not found."
            });
        }

        _dbContext.Products.Remove(product);

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }
}