using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public static class DbSeeder
{
    public static async Task SeedAsync(
        AppDbContext dbContext)
    {
        await dbContext.Database.EnsureCreatedAsync();

        if (!await dbContext.Users.AnyAsync())
        {
            var passwordHasher = new PasswordHasher<AppUser>();

            var admin = new AppUser
            {
                Id = 1,
                Username = "admin",
                Role = "Admin",
                IsActive = true
            };

            admin.PasswordHash = passwordHasher.HashPassword(
                admin,
                "Admin@123");

            var cashier = new AppUser
            {
                Id = 2,
                Username = "cashier",
                Role = "Cashier",
                IsActive = true
            };

            cashier.PasswordHash = passwordHasher.HashPassword(
                cashier,
                "Cashier@123");

            dbContext.Users.AddRange(admin, cashier);
        }

        if (!await dbContext.Products.AnyAsync())
        {
            dbContext.Products.AddRange(
                new Product
                {
                    Id = 1,
                    Name = "Burger",
                    Price = 250,
                    Stock = 20
                },
                new Product
                {
                    Id = 2,
                    Name = "Pizza",
                    Price = 800,
                    Stock = 15
                },
                new Product
                {
                    Id = 3,
                    Name = "Coffee",
                    Price = 150,
                    Stock = 30
                });
        }

        await dbContext.SaveChangesAsync();
    }
}