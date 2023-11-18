using EcommerceAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Xml.Linq;

namespace EcommerceAPI.Data
{
    public class EcommerceContext: DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<CartOrderItem> CartItems { get; set; }

        public DbSet<Address> Addresses { get; set; }




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

            modelBuilder.Entity<CartOrderItem>()
                .HasOne(e => e.Product)
                .WithOne()
                .OnDelete(DeleteBehavior.ClientNoAction);

            modelBuilder.Entity<CartOrderItem>()
                .HasOne(e => e.Customer)
                .WithOne()
                .OnDelete(DeleteBehavior.ClientNoAction);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile($"appsettings.json")
                   .AddJsonFile($"appsettings.{envName}.json", optional: true)
                   .AddUserSecrets(Assembly.GetExecutingAssembly(), true)
                    .AddEnvironmentVariables()
                    .Build();
               
               
                if(envName == "Production") 
                {
                    var server = Environment.GetEnvironmentVariable("Server");
                    var userId = Environment.GetEnvironmentVariable("User_Id");
                    var password = Environment.GetEnvironmentVariable("Password");
                    var database = Environment.GetEnvironmentVariable("Database");
                    var port = Environment.GetEnvironmentVariable("Port");
                    var connection = $"Host={server}User ID={userId};Password={password};Database={database};Port={port};";
                    optionsBuilder.UseNpgsql(connection);
                }else
                {
                    var connectionString = configuration.GetConnectionString("MyConnection");
                
                    optionsBuilder.UseSqlServer(connectionString);
                }
            }
            
        }
    }

    
}
