string productName = "Burger";
decimal price = 250M;
int quantity = 3;

decimal total = price * quantity;

Console.WriteLine($"Product: {productName}");
Console.WriteLine($"Price: {price}");
Console.WriteLine($"Quantity: {quantity}");
Console.WriteLine($"Total Price: {total}");

if (quantity > 0)
{
    Console.WriteLine("Order can be placed.");
}
else
{
    Console.WriteLine("Order cannot be placed.");
}

Console.ReadKey();