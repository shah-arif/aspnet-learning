// using Microsoft.AspNetCore.Mvc;

// [ApiController]
// [Route("api/[controller]")]


// public class ProductsController : ControllerBase
// {
//     private readonly AppDbContext _db;

//     public ProductsController(AppDbContext db)
//     {
//         _db = db;
//     }

//     [HttpGet] // curl -X GET http://localhost:5288/api/products
//     public IActionResult Get()
//     {
//         return Ok(_db.Products);
//     }

//     [HttpGet("{id}")] // curl -X GET http://localhost:5288/api/products/1
//     public IActionResult Get(int id)
//     {
//         var product = _db.Products.FirstOrDefault(p => p.Id == id);
//         if (product == null)
//         {
//             return NotFound();
//         }
//         return Ok(product);
//     }

//     [HttpPost] // curl -X POST -H "Content-Type: application/json" -d '{"name":"product1","price":100}' http://localhost:5288/api/products
//     public IActionResult Post([FromBody] Product product)
//     {
//         _db.Products.Add(product);
//         _db.SaveChanges();
//         return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
//     }

//     [HttpPut("{id}")] // curl -X PUT -H "Content-Type: application/json" -d '{"name":"product1","price":100}' http://localhost:5288/api/products/1
//     public IActionResult Put(int id, [FromBody] Product product)
//     {
//         var existingProduct = _db.Products.FirstOrDefault(p => p.Id == id);
//         if (existingProduct == null)
//         {
//             return NotFound();
//         }
//         existingProduct.Name = product.Name;
//         existingProduct.Price = product.Price;
//         _db.SaveChanges();
//         return NoContent();
//     }

//     [HttpDelete("{id}")]  // curl -X DELETE http://localhost:5288/api/products/1
//     public IActionResult Delete(int id)
//     {
//         var product = _db.Products.FirstOrDefault(p => p.Id == id);
//         if (product == null)
//         {
//             return NotFound();
//         }
//         _db.Products.Remove(product);
//         _db.SaveChanges();
//         return NoContent();
//     }
// }



using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet] // curl -X GET http://localhost:5288/api/products
    public async Task<IActionResult> Get()
    {
        var products = await _productService.GetProducts();
        return Ok(products);
    }

    [HttpGet("{id}")] // curl -X GET http://localhost:5288/api/products/1
    public async Task<IActionResult> Get(int id)
    {
        var product = await _productService.GetProduct(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost] // curl -X POST -H "Content-Type: application/json" -d '{"name":"product1","price":100}' http://localhost:5288/api/products
    public async Task<IActionResult> Post([FromBody] Product product)
    {
        var newProduct = await _productService.CreateProduct(product);
        return CreatedAtAction(nameof(Get), new { id = newProduct.Id }, newProduct);
    }

    [HttpPut("{id}")] // curl -X PUT -H "Content-Type: application/json" -d '{"name":"product1","price":100}' http://localhost:5288/api/products/1
    public async Task<IActionResult> Put(int id, [FromBody] Product product)
    {
        var updatedProduct = await _productService.UpdateProduct(product);
        if (updatedProduct == null)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]  // curl -X DELETE http://localhost:5288/api/products/1
    public async Task<IActionResult> Delete(int id)
    {
        await _productService.DeleteProduct(id);
        return NoContent();
    }
}