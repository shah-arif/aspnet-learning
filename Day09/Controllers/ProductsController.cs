using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private static readonly List<Product> products = new()
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
    
    [HttpGet] // curl localhost:5237/api/products?page=1&pageSize=10
    public IActionResult GetProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;

        var paginatedProducts = products
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var paginatedResponse = new PaginatedProductsResponse
        {
            Products = paginatedProducts,
            Page = page,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(products.Count / (double)pageSize),
            TotalProducts = products.Count
        };

        return Ok(paginatedResponse);
    }

    [HttpGet("{id:int}")] // curl localhost:5237/api/products/1
    public IActionResult GetProduct(int id)
    {
        var product = products.FirstOrDefault(p => p.Id == id);

        if (product is null)
        {
            return NotFound(new { message = $"Product with id {id} not found" });
        }

        return Ok(product);
    }

    [HttpPost] // curl -X POST localhost:5237/api/products -H "Content-Type: application/json" -d '{"name":"Apple","price":1.2,"stock":10,"category":"Fruit"}'
    public IActionResult CreateProduct(CreateProductRequest request)
    {
        var product = new Product
        {
            Id = products.Max(p => p.Id) + 1,
            Name = request.Name,
            Price = request.Price,
            Stock = request.Stock,
            Category = request.Category
        };

        products.Add(product);
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id} , product);
    }

    [HttpPut("{id:int}")] // curl -X PUT localhost:5237/api/products/1 -H "Content-Type: application/json" -d '{"name":"Apple","price":1.2,"stock":10,"category":"Fruit"}'
    public IActionResult UpdateProduct(int id, UpdateProductRequest request)
    {
        var product = products.FirstOrDefault(p => p.Id == id);

        if (product is null)
        {
            return NotFound(new { message = $"Product with id {id} not found" });
        }

        product.Name = request.Name;
        product.Price = request.Price;
        product.Stock = request.Stock;
        product.Category = request.Category;

        return Ok(product);
    }

    [HttpPatch("{id:int}")] // curl -X PATCH localhost:5237/api/products/1 -H "Content-Type: application/json" -d '{"name":"Apple","price":1.2,"stock":10,"category":"Fruit"}'
    public IActionResult PatchProduct(int id, PatchProductRequest request)
    {
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product is null)
        {
            return NotFound(new { message = $"Product with id {id} not found" });
        }

        if (request.Name is not null) product.Name = request.Name;
        if (request.Price is not null) product.Price = request.Price.Value;
        if (request.Stock is not null) product.Stock = request.Stock.Value;
        if (request.Category is not null) product.Category = request.Category;

        return Ok(product);
    }

    [HttpDelete("{id:int}")] // curl -X DELETE localhost:5237/api/products/1
    public IActionResult DeleteProduct(int id)
    {
        var product = products.FirstOrDefault(p => p.Id == id);

        if (product is null)
        {
            return NotFound(new { message = $"Product with id {id} not found" });
        }

        products.Remove(product);
        return NoContent();
    }

    [HttpGet("search")] // curl localhost:5237/api/products/search?category=Fruit
    public IActionResult SearchProducts(string? category)
    {
        var query = products.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(category))
        {
            query = query.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
        }

        return Ok(query.ToList());
    }

    [HttpGet("filter")] // curl localhost:5237/api/products/filter?category=Fruit&minPrice=1.2&maxPrice=1.9
    public IActionResult FilterProducts(string? category, decimal? minPrice, decimal? maxPrice)
    {
        var query = products.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(category))
        {
            query = query.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
        }
        
        if (minPrice.HasValue)
        {
            query = query.Where(p => p.Price >= minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= maxPrice.Value);
        }

        return Ok(query.ToList());
    }
}