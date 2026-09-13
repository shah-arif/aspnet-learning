

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _db;

    public ProductsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetProducts()
    {
        var products = _db.Products.ToList();

        return Ok(products);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteProduct(int id)
    {
        var product = _db.Products.Find(id);

        if (product is null)
        {
            return NotFound();
        }

        _db.Products.Remove(product);
        _db.SaveChanges();

        return NoContent();
    }

    [HttpGet("me")]
    [Authorize]
    public IActionResult Me()
    {
        return Ok(new
        {
            username = User.Identity?.Name,
            isAuthenticated = User.Identity?.IsAuthenticated,
            role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value
        });
    }
}