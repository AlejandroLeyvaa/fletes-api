using Fletes.Models;
using Microsoft.EntityFrameworkCore;
using Route = Fletes.Models.Route;

namespace Fletes.Context
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DbSet for each entity in the freight management system
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Freight> Freights { get; set; }
        public DbSet<FreightProduct> FreightProducts { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        // Configuring the entity relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureFreightProduct(modelBuilder);
            ConfigureFreight(modelBuilder);
            ConfigureCustomer(modelBuilder);
            ConfigureVehicle(modelBuilder);

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected void ConfigureFreightProduct(ModelBuilder modelBuilder)
        {
            // Many-to-Many relationship between Freight and Product
            modelBuilder.Entity<FreightProduct>()
                .HasKey(fp => new { fp.FreightId, fp.ProductId });

            modelBuilder.Entity<FreightProduct>()
                .HasOne(fp => fp.Freight)
                .WithMany(f => f.FreightProducts)
                .HasForeignKey(fp => fp.FreightId);

            modelBuilder.Entity<FreightProduct>()
                .HasOne(fp => fp.Product)
                .WithMany(p => p.FreightProducts)
                .HasForeignKey(fp => fp.ProductId);
        }

        protected void ConfigureFreight(ModelBuilder modelBuilder)
        {
            // One-to-Many relationships
            modelBuilder.Entity<Freight>()
                .HasOne(f => f.Customer)
                .WithMany(c => c.Freights)
                .HasForeignKey(f => f.CustomerId);

            modelBuilder.Entity<Freight>()
                .HasOne(f => f.Carrier)
                .WithMany(c => c.Freights)
                .HasForeignKey(f => f.CarrierId);

            modelBuilder.Entity<Freight>()
                .HasOne(f => f.Vehicle)
                .WithMany(v => v.Freights)
                .HasForeignKey(f => f.VehicleId);

            modelBuilder.Entity<Freight>()
                .HasOne(f => f.Route)
                .WithMany(r => r.Freights)
                .HasForeignKey(f => f.RouteId);
        }

        protected void ConfigureCustomer(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Phone)
                .IsUnique();
        }

        protected void ConfigureVehicle(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>()
                .HasIndex(v => v.Status)
                .IsUnique();
        }

    }
}
