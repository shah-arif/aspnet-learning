public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Category> Categories { get; set; } = new List<Category>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<RestaurantTable> Tables { get; set; } = new List<RestaurantTable>();
}