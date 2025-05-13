using Core.Entities.Concrete;
using Entities.Concrete.Orders;
using Entities.Concrete.Preferences;
using Entities.Concrete.Products;
using Entities.Concrete.Users;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework;

public class DataBaseContext : DbContext
{    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=SoftwareDevDb.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuring relationships
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasOne<Brand>().WithMany().HasForeignKey(p => p.BrandId);
            entity.HasOne<Category>().WithMany().HasForeignKey(p => p.CategoryId);
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasOne<Cart>().WithMany(c => c.Items).HasForeignKey(ci => ci.CartId);
            entity.HasOne<Product>().WithMany().HasForeignKey(ci => ci.ProductId);
        });

        modelBuilder.Entity<Wishlist>(entity =>
        {
            entity.HasMany(w => w.Items).WithMany();
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasOne<Product>().WithMany().HasForeignKey(r => r.ProductId);
            entity.HasOne<Customer>().WithMany().HasForeignKey(r => r.CustomerId);
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasOne<StaffType>().WithMany().HasForeignKey(s => s.TypeId);
        });

        modelBuilder.Entity<UserOperationClaim>(entity =>
        {
            entity.HasOne<User>().WithMany().HasForeignKey(u => u.UserId);
            entity.HasOne<OperationClaim>().WithMany().HasForeignKey(u => u.OperationClaimId);
        });

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Cart>? Carts { get; set; }
    public DbSet<CartItem>? CartItems { get; set; }
    public DbSet<Order>? Orders { get; set; }
    public DbSet<Payment>? Payments { get; set; }
    public DbSet<Address>? Addresses { get; set; }
    public DbSet<Wishlist>? Wishlists { get; set; }
    public DbSet<Brand>? Brands { get; set; }
    public DbSet<Category>? Categories { get; set; }
    public DbSet<Discount>? Discounts { get; set; }
    public DbSet<Product>? Products { get; set; }
    public DbSet<Review>? Reviews { get; set; }
    public DbSet<Staff>? Staffes { get; set; }
    public DbSet<Customer>? Customers { get; set; }
    public DbSet<StaffType>? StaffTypes { get; set; }
    public DbSet<User>? Users { get; set; }
    public DbSet<OperationClaim>? OperationClaims { get; set; }
    public DbSet<UserOperationClaim>? UserOperationClaims { get; set; }
}