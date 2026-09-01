public class OrderResponseDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Total { get; set; }
    public List<OrderItemResponseDto> Items { get; set; } = new();
}