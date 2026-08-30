using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet] // curl -X GET http://localhost:5000/api/order
    public async Task<IActionResult> GetAllAsync()
    {
        var orders = await _orderService.GetAllAsync();
        return Ok(orders);
    }

    [HttpGet("{id}")] // curl -X GET http://localhost:5000/api/order/1
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var order = await _orderService.GetByIdAsync(id);
        if (order == null)
        {
            return NotFound();
        }
        return Ok(order);
    }

    [HttpPost] // curl -X POST http://localhost:5000/api/order -d '{"items":[{"productId":1,"quantity":1},{"productId":2,"quantity":2}]}'
    public async Task<IActionResult> CreateAsync([FromBody] OrderCreateDto dto)
    {
        var order = await _orderService.CreateAsync(dto);
        return Ok(order);
    }

}
