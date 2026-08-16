// Product product = new Product(
//     1,
//     "Apple",
//     1.99m
// );

// Console.WriteLine(product.Name);
// Console.WriteLine(product.Price);
// class Product
// {
//     public int Id { get; set; }
//     public string Name { get; set; }
//     public decimal Price { get; set; }
//     public Product(int id, string name, decimal price)
//     {
//         this.Id = id;
//         this.Name = name;
//         this.Price = price;
//     }
// }

// Product product = new Product();
// product.Name = "Apple";
// product.Price = 1.99m;
// product.Display();

// class Product
// {
//     public string? Name { get; set; }
//     public decimal Price { get; set; }

//     public void Display()
//     {
//         Console.WriteLine($"{Name } - {Price}");
//     }
// }


// Product product = new Product
// {
//     Id = 1,
//     Name = "Apple",
//     Price = 1.99m,
//     Stock = 10
// };

// // bool available = product.HasStock(11);
// // Console.WriteLine(available);

// product.IncreaseStock(10);
// Console.WriteLine(product.Stock);

// Dog dog = new Dog();
// dog.Eat();
// dog.Bark();

Animal animal = new Dog();
animal.Speak();