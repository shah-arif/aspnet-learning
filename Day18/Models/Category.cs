public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; } = new Restaurant();
    public ICollection<Product> Products { get; set; } = new List<Product>();
}