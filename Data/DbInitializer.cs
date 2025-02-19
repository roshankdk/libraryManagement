using LibraryManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            // Apply any pending migrations
            context.Database.Migrate();

            // Create roles
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roles = { "Admin", "Librarian", "Member" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Create default admin if not exists
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string adminEmail = "admin@library.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser { UserName = adminEmail, Email = adminEmail };
                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Seed sample books if none exist
            if (!context.Books.Any())
            {
                context.Books.AddRange(
                    new Book { Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Genre = "Fiction", Description = "A classic novel.", IsAvailable = true },
                    new Book { Title = "1984", Author = "George Orwell", Genre = "Dystopian", Description = "A dystopian novel.", IsAvailable = true },
                    new Book { Title = "To Kill a Mockingbird", Author = "Harper Lee", Genre = "Fiction", Description = "A novel about racism and injustice.", IsAvailable = true }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
