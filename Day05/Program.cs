// // static int Add(int a, int b)
// // {
// //     return a + b;
// // }

// // Calculator calculator = Add;
// Calculator calculator = (a, b) => a + b;
// int result = calculator(10, 20);
// Console.WriteLine(result);
// delegate int Calculator(int a, int b);


// // int reuslt = Add(10, 20);
// // Console.WriteLine(reuslt);

// Action sayHello = () => Console.WriteLine("Hello");
// sayHello();

// Action<string> printName = name => Console.WriteLine(name);
// printName("Abdullah");

// Action<string, int> displayProduct = (name, price) =>
// {
//     Console.WriteLine($"{name} - {price}");
// };

// displayProduct("Apple", 250);

// Func<int, int, int> add = (a, b) => a + b;

// int result = add(10, 20);
// Console.WriteLine(result);

// Func<decimal, decimal, decimal> calculateTotal = (price, quantity) => price * quantity;
// decimal total = calculateTotal(250m, 2);
// Console.WriteLine(total);

// Predicate<int> isEven = num => num % 2 == 0;
// Console.WriteLine(isEven(11));

// Predicate<Product> isExpensive = product => product.Price >= 250m;
// Product product = new Product { Id = 1, Name = "Apple", Price = 150m, Stock = 10, Category = "Fruit" };
// bool reuslt = isExpensive(product);
// Console.WriteLine(reuslt);



// string name = "Abdullah";
// bool isLong = name.IsLong();
// Console.WriteLine(isLong);



// public static class StringExtensions
// {
//     public static bool IsLong(this string value)
//     {
//         return value.Length > 10;
//     }
// }

// decimal price = 250m;
// if (price.IsPositive())
// {
//     Console.WriteLine("Price is positive");
// }
// else
// {
//     Console.WriteLine("Price is not positive");
// }

// public static class DecimalExtensions
// {
//     public static bool IsPositive(this decimal value)
//     {
//         return value > 0;
//     }

//     public static bool IsNegative(this decimal value)
//     {
//         return value < 0;
//     }

//     public static bool IsZero(this decimal value)
//     {
//         return value == 0;
//     }

//     public static bool IsNotZero(this decimal value)
//     {
//         return value != 0;
//     }

//     public static bool IsBetween(this decimal value, decimal min, decimal max)
//     {
//         return value >= min && value <= max;
//     }

//     public static bool IsNotBetween(this decimal value, decimal min, decimal max)
//     {
//         return value < min || value > max;
//     }

//     public static bool IsOdd(this decimal value)
//     {
//         return value % 2 == 1;
//     }


// }

// ProductDto product = new ProductDto(1, "Apple", 250m);
// Console.WriteLine(product.Id);
// Console.WriteLine(product.Name);

// var a = new ProductDto(1, "Apple", 250m);
// var b = new ProductDto(1, "Apple", 250m);

// Console.WriteLine(a == b);


// public record ProductDto(
//     int Id,
//     string Name,
//     decimal Price
// );


// Product product = null!;
// product = new Product { Id = 1, Name = "Apple", Price = 250m };

// // if (product != null)
// if (product is not null)
// {
//     if (product.Price is decimal price)
//     {
//         Console.WriteLine(price);
//     }
// }
// else
// {
//     Console.WriteLine("Product is null");
// }

// string GetStatusMessage(string status)
// {
//     return status switch
//     {
//         "Pending" => "Order is pending",
//         "Processing" => "Order is being processed",
//         "Shipped" => "Order has been shipped",
//         "Delivered" => "Order has been delivered",
//         "Canceled" => "Order has been canceled",
//         _ => "Unknown status"
//     };
// }

// Console.WriteLine(GetStatusMessage("Shipped"));

// var value = 10;
// value = "Hello"; // Cannot implicitly convert type 'string' to 'int'

// dynamic value = 10;
// value = "Hello";
// value = true;

// Console.WriteLine(value);
