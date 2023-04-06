using System.Text.Json;
using Entities;
using Entities.Documents;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class Seed
{
    public static async Task SeedData(DataContext context, UserManager<User> userManager)
    {
        

           if (!context.Users.Any())
        {
            var _user = setUser();
            _user.EmailConfirmed = true;
            userManager.CreateAsync(_user, "P@ssword1");
        }

        if (!context.DocumentTypes.Any())
        {
            var typesData = await File.ReadAllTextAsync("seed_JSONs/documentTypes.json");
            var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
            var documentTypes = JsonSerializer.Deserialize<List<DocumentType>>(typesData);
            if (documentTypes != null)
            await context.DocumentTypes.AddRangeAsync(documentTypes);
            context.SaveChanges();
        }

        if (!context.Categories.Any())
        {
            var categoriesData = await File.ReadAllTextAsync("seed_JSONs/category.json");
            var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
            var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);
            if (categories != null)
            await context.Categories.AddRangeAsync(categories);
            context.SaveChanges();
        }

        if (!context.Customers.Any())
        {
            var customersData = await File.ReadAllTextAsync("seed_JSONs/customers.json");
            var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
            var customers = JsonSerializer.Deserialize<Customer>(customersData);
            if (customers != null)
            await context.Customers.AddAsync(customers);
            context.SaveChanges();
        }
}

    private static User setUser()
    {
        var user = new User
        {
            UserName = "admin",
            FirstName= "admin",
            LastName= "admin",
            Email = "admin@test.com"
        };

        return user;
    }
}
