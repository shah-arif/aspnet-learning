public class Order
{
    public int Id { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public decimal Total { get; set; }
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; } = new Restaurant();
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}