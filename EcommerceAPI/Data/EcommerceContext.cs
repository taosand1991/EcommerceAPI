using EcommerceAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace EcommerceAPI.Data
{
    public class EcommerceContext: DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public EcommerceContext(DbContextOptions<EcommerceContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany(x => x.Products)
                .WithOne()
                .HasForeignKey(x => x.CustomerId)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .HasOne<Customer>()
                .WithMany(e => e.Products)
                .HasForeignKey(e => e.CustomerId)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Categories)
                .WithMany(e => e.Products)
                .UsingEntity("CategoryProduct");

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Products)
                .WithMany(e => e.Categories)
                .UsingEntity("CategoryProduct");
        }
    }

    
}
