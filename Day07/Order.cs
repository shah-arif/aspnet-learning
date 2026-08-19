public class Order
{
    public int Id { get; }
    public string OrderNumber { get; }
    public string CustomerName { get; }
    public List<OrderItem> Items { get; }
    public bool IsCancelled { get; private set; }

    public Order(int id, string orderNumber, string customerName)
    {
        Id = id;
        OrderNumber = orderNumber;
        CustomerName = customerName;

        Items = new List<OrderItem>();
    }

    public void AddItem(Product product, int quantity)
    {
        if (IsCancelled)
            throw new InvalidOperationException("Order is cancelled");


        if (!product.HasStock(quantity))
            throw new InvalidOperationException($"Not enough stock for {product.Name}");
        
        bool success = product.DecreaseStock(quantity);
        if (!success)
            throw new InvalidOperationException($"Not enough stock for {product.Name}");

        Items.Add(new OrderItem(product, quantity));
    }

    public decimal CalculateTotal()
    {
        return Items.Sum(item => item.Total);
    }

    public void Cancel()
    {
        if (IsCancelled)
            return;
        
        IsCancelled = true;

        foreach (var item in Items)
        {
            item.Product.IncreaseStock(item.Quantity);
        }
    }

}