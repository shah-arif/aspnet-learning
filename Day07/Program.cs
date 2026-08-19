List<Product> products = new()
{
    new Product(1, "Burger", 250m, 10),
    new Product(2, "Pizza", 500m, 5),
    new Product(3, "Coffee", 150m, 20),
    new Product(4, "Sandwich", 200m, 8),
};

void CurrentStock()
{
    Console.WriteLine("Current stock:");
    foreach (var product in products)
    {
        Console.WriteLine(
            $"{product.Id}. {product.Name} - " +
            $"৳{product.Price} - Stock: {product.Stock}"
        );
    }
}

CurrentStock();

var expensiveProducts = products
    .Where(p => p.Price > 200)
    .OrderByDescending(p => p.Price)
    .ToList();

Console.WriteLine("Expensive products:");
foreach (var product in expensiveProducts)
{
    Console.WriteLine(product.Name);
}

var burger = products
    .FirstOrDefault(p => p.Name == "Burger");
if (burger == null)
{
    Console.WriteLine("Product not found");  
}
else
{
    Console.WriteLine($"Product found: {burger.Name}");
}


var order = new Order(1, "ORD-1001", "Abdullah");
order.AddItem(burger!, 2);

var pizza = products
    .FirstOrDefault(p => p.Name == "Pizza");
if (pizza is not null)
{
    try
    {
        order.AddItem(pizza!, 1);
    } catch (InvalidOperationException ex)
    {
        Console.WriteLine($"Order error: {ex.Message}");
    }
}
decimal total = order.CalculateTotal();

Console.WriteLine(
    $"Order total: ৳{total}"
);
CurrentStock();

order.Cancel();
CurrentStock();

var productService = new ProductService(products);

var allProducts = await productService.GetProductsAsync();
Console.WriteLine("All products:");
foreach (var product in allProducts)
{
    Console.WriteLine(product.Name);
}

var productById = await productService.GetProductByIdAsync(2);
if (productById is not null)
{
    Console.WriteLine($"Product found: {productById.Name}");
}