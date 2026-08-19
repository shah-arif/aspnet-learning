public class Product
{
    public int Id { get; }
    public string Name { get; }
    public decimal Price { get; }
    public int Stock { get; private set; }

    public Product(int id, string name, decimal price, int stock)
    {
        Id = id;
        Name = name;
        Price = price;
        Stock = stock;
    }

    public void IncreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than 0");
        Stock += quantity;
    }

    public bool DecreaseStock(int quantity)
    {
        if (quantity <= 0)
            return false;
        if (quantity > Stock)
            return false;
        Stock -= quantity;
        return true;
    }

    public bool HasStock(int quantity)
    {
        return quantity > 0 && quantity <= Stock;
    }
}