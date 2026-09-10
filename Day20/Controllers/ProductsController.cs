using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]

public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }

    [HttpGet] // curl -s -X GET 'http://localhost:5002/api/products?search=carrot' | jq
    public async Task<IActionResult> GetAll([FromQuery] ProductQueryDto request)
    {
        var products = await _service.GetAllAsync(request);
        return Ok(products);
    }

    [HttpGet("{id}")] // curl -s -X GET 'http://localhost:5002/api/products/1' | jq
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _service.GetByIdAsync(id);

        return Ok(product);
    }

    [HttpPost] // curl -s -X POST 'http://localhost:5002/api/products' -H 'Content-Type: application/json' -d '{"name":"Carrot","description":"A good carrot","price":100,"stock":10,"categoryId":1}' | jq
    public async Task<IActionResult> Create(ProductCreateDto dto)
    {
        var product = await _service.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpPut("{id:int}")] // curl -s -X PUT 'http://localhost:5002/api/products/1' -H 'Content-Type: application/json' -d '{"name":"test","price":100,"stock":10,"category":"test"}' | jq
    public async Task<IActionResult> Update(int id, ProductUpdateDto dto)
    {
        await _service.UpdateAsync(id, dto);

        return NoContent();
    }

    [HttpDelete("{id:int}")] // curl -s -X DELETE 'http://localhost:5002/api/products/1' | jq
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);

        return NoContent();
    }

    
}