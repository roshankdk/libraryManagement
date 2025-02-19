using LibraryManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<BorrowRecord> BorrowRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the Book entity
            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.Title).HasColumnType("TEXT");
                entity.Property(e => e.Author).HasColumnType("TEXT");
                entity.Property(e => e.Genre).HasColumnType("TEXT");
                entity.Property(e => e.Description).HasColumnType("TEXT");
            });
        }
    }
}
