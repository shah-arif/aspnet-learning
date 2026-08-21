using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public IActionResult GetProducts()
    {
        var products = _productService.GetAll();

        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetProductById(int id)
    {
        var product = _productService.GetById(id);

        if (product is null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost] // curl -X POST http://localhost:5172/api/products -H "Content-Type: application/json" -d '{"name":"Apple","price":1.2,"stock":10,"category":"Fruit"}'
    public IActionResult CreateProduct(Product product)
    {
        var createdProduct = _productService.Create(product);

        return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateProduct(int id, Product product)
    {
        var updated = _productService.Update(id, product);

        if (!updated)
        {
            return NotFound();
        }

        return Ok(_productService.GetById(id));
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteProduct(int id)
    {
        var deleted = _productService.Delete(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}