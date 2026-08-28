using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]

public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoriesController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet] // curl -s -X GET 'http://localhost:5002/api/categories' | jq
    public async Task<IActionResult> GetAll()
    {
        var categories = await _service.GetAllAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")] // curl -s -X GET 'http://localhost:5002/api/categories/1' | jq
    public async Task<IActionResult> GetById(int id)
    {
        var category = await _service.GetByIdAsync(id);

        if (category is null)
        {
            return NotFound(new { message = "Category not found" });
        }

        return Ok(category);
    }

    [HttpPost] // curl -s -X POST 'http://localhost:5002/api/categories' -H 'Content-Type: application/json' -d '{"name":"test"}' | jq
    public async Task<IActionResult> Create(CategoryCreateDto dto)
    {
        var category = await _service.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
    }

    // GET /api/categories/{id}/products
    [HttpGet("{id:int}/products")] // curl -s -X GET 'http://localhost:5002/api/categories/1/products' | jq
    public async Task<IActionResult> GetAllByCategory(int id)
    {
        var products = await _service.GetAllByCategoryAsync(id);

        return Ok(products);
    }


}