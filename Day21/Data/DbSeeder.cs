public static class DbSeeder
{
    public static void Seed(AppDbContext db)
    {
        if (!db.Users.Any())
        {
            db.Users.AddRange(
                new User
                {
                    Username = "user",
                    Password = "123456",
                    Role = "User"
                },
                new User
                {
                    Username = "admin",
                    Password = "123456",
                    Role = "Admin"
                }
            );
        }

        if (!db.Products.Any())
        {
            db.Products.AddRange(
                new Product
                {
                    Name = "Burger",
                    Price = 250,
                    Stock = 20
                },
                new Product
                {
                    Name = "Pizza",
                    Price = 500,
                    Stock = 10
                }
            );
        }

        db.SaveChanges();
    }
}