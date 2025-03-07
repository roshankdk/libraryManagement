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
            string adminEmail = "admin@patshala.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser { UserName = adminEmail, Email = adminEmail };
                var result = await userManager.CreateAsync(adminUser, "Random@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Seed sample books if none exist
            if (!context.Books.Any())
            {
                context.Books.AddRange(
                    new Book { Title = "The Pragmatic Programmer", Author = "Andrew Hunt", Genre = "Programming", Description = "A guide to becoming a better programmer.", IsAvailable = true },
                    new Book { Title = "Clean Code", Author = "Robert C. Martin", Genre = "Programming", Description = "A handbook of agile software craftsmanship.", IsAvailable = true },
                    new Book { Title = "Refactoring", Author = "Martin Fowler", Genre = "Programming", Description = "Improving the design of existing code.", IsAvailable = true },
                    new Book { Title = "Code Complete", Author = "Steve McConnell", Genre = "Programming", Description = "A practical handbook of software construction.", IsAvailable = true }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
