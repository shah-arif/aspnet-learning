public class RestaurantTable
{
    public int Id { get; set; }
    public string TableNumber { get; set; } = string.Empty;
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; } = new Restaurant();
}