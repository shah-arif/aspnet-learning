// string productName = "Burger";
// decimal price = 250M;
// int quantity = 3;

// decimal total = price * quantity;

// Console.WriteLine($"Product: {productName}");
// Console.WriteLine($"Price: {price}");
// Console.WriteLine($"Quantity: {quantity}");
// Console.WriteLine($"Total Price: {total}");

// if (quantity > 0)
// {
//     Console.WriteLine("Order can be placed.");
// }
// else
// {
//     Console.WriteLine("Order cannot be placed.");
// }

// Console.ReadKey();


// static void Greet(string name)
// {
//     Console.WriteLine("Hello " + name);
// }

// Greet("Abdullah");

// static decimal CalculateFinalPrice(decimal price, int quantity, decimal discountPercentage)
// {
//     return price * quantity * (1 - discountPercentage / 100);
// }
// decimal finalPrice = CalculateFinalPrice(500M, 2, 10);
// Console.WriteLine($"Final Price: {finalPrice}");


Product product = new Product();


class Product
{
    public int id { get; set; }
    public string name { get; set; }

    public Product()
    {
        Console.WriteLine("Product constructor");
    }
}

