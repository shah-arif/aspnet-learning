public class OrderCreateDto
{
    public List<OrderItemCreateDto> Items { get; set; } = new();
}