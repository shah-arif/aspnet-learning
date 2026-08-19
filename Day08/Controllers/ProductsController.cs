using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetProducts()
    {
        var products = new[]
        {
            new
            {
                Id = 1,
                Name = "Burger",
                Price = 250
            },
            new
            {
                Id = 2,
                Name = "Pizza",
                Price = 500
            }
        };
        return Ok(products);
    }

    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        var products = new[]
        {
            new
            {
                Id = 1,
                Name = "Burger",
                Price = 250
            },

            new
            {
                Id = 2,
                Name = "Pizza",
                Price = 500
            }
        };
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product is null)
        {
            return NotFound("Product not found");
        }
        var response = new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
        };
        return Ok(response);
    }

    // E.G. GET /api/products/search?name=burger
    [HttpGet("search")]
    public IActionResult SearchProducts(string name)
    {
        var products = new[]
        {
            new
            {
                Id = 1,
                Name = "Burger",
                Price = 250
            },
            new
            {
                Id = 2,
                Name = "Pizza",
                Price = 500
            }
        };
        var result = products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToArray();
        return Ok(result);
    }


    // [HttpPost]
    // public IActionResult CreateProduct()
    // {
    //     var message = "Product created";
    //     return Ok(message);
    // }
    // //curl -X POST http://localhost:5062/api/products -H "Content-Type: application/json" -d '{"name":"Burger","price":250}'

    [HttpPost]
    public IActionResult CreateProduct(CreateProductRequest request)
    {
        var newProduct = new
        {
            Id = 1,
            Name = request.Name,
            Price = request.Price,
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };
        return CreatedAtAction("GetProduct", new { id = newProduct.Id }, newProduct);
    }
    //curl -X POST http://localhost:5062/api/products -H "Content-Type: application/json" -d '{"name":"Burger","price":250}'
}

public class CreateProductRequest
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}

public class ProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}