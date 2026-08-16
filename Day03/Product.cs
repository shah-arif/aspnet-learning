public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public void IncreaseStock(int quantity)
    {
        Stock += quantity;
    }
    public void DecreaseStock(int quantity)
    {
        Stock -= quantity;
    }
    public bool HasStock(int quantity)
    {
        return Stock >= quantity;
    }
}