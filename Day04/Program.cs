// string[] prodcuts =
// {
//     "Apple",
//     "Orange",
//     "Banana",
//     "Mango",
//     "Pineapple",
// };

// foreach (var product in prodcuts)
// {
//     Console.WriteLine(product);
// }


// string[] products = new string[3];
// products[0] = "Apple";
// products[1] = "Orange";
// products[2] = "Banana";
// // products[3] = "Mango";
// // products[4] = "Pineapple";

// foreach (var product in products)
// {
//     Console.WriteLine(product);
// }

// // List<string> products = new List<string>();
// List<string> products = new();

// products.Add("Apple");
// products.Add("Orange");
// products.Add("Banana");
// products.Add("Mango");
// products.Add("Pineapple");

// foreach (var product in products)
// {
//     Console.WriteLine(product);
// }

// List<string> products = new();

// products.Add("Apple");
// products.Add("Orange");

// products.Remove("Apple");

// Console.WriteLine(products.Count);

// Console.WriteLine(products[0]);

// if (products.Contains("Apple"))
// {
//     Console.WriteLine("Apple is in the list");
// }

// products.Clear();

// Console.WriteLine(products.Count);


// List<Product> products = new();

// products.Add(new Product
// {
//     Id = 1,
//     Name = "Apple",
//     Price = 250m
// });
// products.Add(new Product
// {
//     Id = 2,
//     Name = "Orange",
//     Price = 250m
// });

// Console.WriteLine(products.Count);

// foreach (var product in products)
// {
//     Console.WriteLine(product.Name + " " + product.Price);
// }

// Dictionary<int, string> users = new();

// users.Add(1, "Abdullah");
// users.Add(2, "Mohammad");
// users.Add(3, "Ahmed");
// users.Add(4, "Mohammad");
// users.Add(5, "Ahmed");
// users.Add(6, "Mohammad");
// users.Add(7, "Ahmed");
// users.Add(8, "Mohammad");
// users.Add(9, "Ahmed");

// foreach (var user in users)
// {
//     Console.WriteLine(user.Key + " " + user.Value);
// }

// for (int i = 1; i <= users.Count; i++)
// {
//     Console.WriteLine( i + " " + users[i]);
// }

// Dictionary<int, Product> products = new();

// products.Add(1, new Product
// {
//     Id = 1,
//     Name = "Apple",
//     Price = 250m
// });
// products.Add(2, new Product
// {
//     Id = 2,
//     Name = "Orange",
//     Price = 250m
// });

// Product? product = products[2];

// Console.WriteLine(product.Name + " " + product.Price);

// Box<string> name = new();
// name.Value = "Abdullah";
// Console.WriteLine(name.Value);

// Box<int> age = new();
// age.Value = 25;
// Console.WriteLine(age.Value);
// public class Box<T>
// {
//     public T Value { get; set; } = default!;
// }

// int? age = null;

// DateTime? date = null;

// age = 25;
// date = DateTime.Now;

// Console.WriteLine(age);
// Console.WriteLine(date);

// int a = 10;
// int b = 0;


// try
// {
//     int result = a / b;
// }
// catch (Exception ex)
// {
//     Console.WriteLine(ex.Message);
// }
// finally
// {
//     b = 1;
//     int result = a / b;
//     Console.WriteLine(result);
// }


// string input = "12";

// if (int.TryParse(input, out int age))
// {
//     Console.WriteLine(age.GetType());
// }
// else
// {
//     Console.WriteLine("Invalid input");
// }

// Console.WriteLine(int.Parse(input));


List<Product> products = new()
{
    new Product { Id = 1, Name = "Apple", Price = 250m },
    new Product { Id = 2, Name = "Orange", Price = 50m },
    new Product { Id = 3, Name = "Banana", Price = 112m },
    new Product { Id = 4, Name = "Mango", Price = 340m },
    new Product { Id = 5, Name = "Pineapple", Price = 60m },
};

// List<Product> result = new();

// foreach (var product in products)
// {
//     if (product.Price > 100m)
//     {
//         result.Add(product);
//     }
// }

// With LINQ
var result = products
    .Where(p => p.Price > 100m)
    .ToList();

var expensiveProducts = products
    .Where(p => p.Price > 300m)
    .ToList();

var names = products
    .Select(p => p.Name)
    .ToList();

var sortedProducts = products
    .OrderBy(p => p.Price)
    // .OrderByDescending(p => p.Price)
    .ToList();


var newProducts = sortedProducts;

var newProducts2 = sortedProducts
    .FirstOrDefault(p => p.Price > 100m);

bool exists = sortedProducts.Any(p => p.Price > 600m);

// Console.WriteLine(result.Count);


// Console.WriteLine($"Expensive products: {expensiveProducts.Count}");

// foreach (var product in expensiveProducts)
// {
//     Console.WriteLine(product.Name + " " + product.Price);
// }


// foreach (var product in result)
// {
//     Console.WriteLine(product.Name + " " + product.Price);
// }

// names.ForEach(name => Console.WriteLine(name));

// foreach (var name in names)
// {
//     Console.WriteLine(name);
// }


// foreach (var product in sortedProducts)
// {
//     Console.WriteLine(product.Name + " " + product.Price);
// }

// Console.WriteLine(exists);
// if (exists)
// {
//     Console.WriteLine("Products exist");
// }
// else
// {
//     Console.WriteLine("Products do not exist");
// }

// int count = products.Count;

// int expensiveCount = products.Count(p => p.Price > 300m);

// Console.WriteLine(expensiveCount);

// decimal total = products.Sum(p => p.Price);

// Console.WriteLine($"Total price: {total}");

// decimal average = products.Average(p => p.Price);

// Console.WriteLine($"Average price: {average}");

// decimal max = products.Max(p => p.Price);

// Console.WriteLine($"Max price: {max}");

// decimal min = products.Min(p => p.Price);

// Console.WriteLine($"Min price: {min}");


var chainResult = products
    .Where(p => p.Price > 100m)
    .OrderByDescending(p => p.Price)
    .Select(p => p.Name)
    .ToList();

// foreach (var name in chainResult)
// {
//     Console.WriteLine(name);
// }

chainResult.ForEach(name => Console.WriteLine(name));