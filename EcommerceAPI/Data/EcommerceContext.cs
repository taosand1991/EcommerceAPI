using EcommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Data
{
    public class EcommerceContext: DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

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
        }
    }

    
}
