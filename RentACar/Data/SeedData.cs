using Microsoft.AspNetCore.Identity;
using RentACar.Data.Models;

namespace RentACar.Data;

/// <summary>
/// Зарежда начални данни в базата (роли, администратор, примерни коли).
/// </summary>
public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

        // Прилагаме миграциите автоматично
        await context.Database.EnsureCreatedAsync();

        // Създаваме роли
        string[] roles = { "Admin", "User" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // Създаваме администратор по подразбиране
        const string adminEmail = "admin@rentacar.bg";
        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var admin = new ApplicationUser
            {
                UserName = "admin",
                Email = adminEmail,
                FirstName = "Администратор",
                LastName = "Системен",
                Egn = "0000000000",
                PhoneNumber = "0888000000",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(admin, "Admin123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }

        // Добавяме примерни коли ако базата е празна
        if (!context.Cars.Any())
        {
            context.Cars.AddRange(
                new Car
                {
                    Make = "Toyota",
                    Model = "Yaris",
                    Year = 2022,
                    Seats = 5,
                    Description = "Икономична градска кола, климатик, автоматик",
                    PricePerDay = 45.00m
                },
                new Car
                {
                    Make = "BMW",
                    Model = "320i",
                    Year = 2023,
                    Seats = 5,
                    Description = "Луксозен седан, кожен салон, навигация",
                    PricePerDay = 120.00m
                },
                new Car
                {
                    Make = "Dacia",
                    Model = "Duster",
                    Year = 2021,
                    Seats = 5,
                    Description = "SUV, 4x4, подходящ за терен",
                    PricePerDay = 65.00m
                }
            );
            await context.SaveChangesAsync();
        }
    }
}
