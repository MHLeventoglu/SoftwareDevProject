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
            optionsBuilder.UseSqlite("Data Source=RoboSales.db");
        }
    }    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Base User Configuration with TPH (Table Per Hierarchy) inheritance

        // User Related //
        modelBuilder.Entity<User>(entity =>
        {
            // Primary Key Configuration
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Id).ValueGeneratedOnAdd();
            
            // Required Fields
            entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
            entity.Property(u => u.PasswordHash).IsRequired();
            entity.Property(u => u.FirstName).IsRequired().HasMaxLength(50);
            entity.Property(u => u.Surname).IsRequired().HasMaxLength(50);
            entity.Property(u => u.DateAdded).HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Configure Discriminator for TPH inheritance
            entity.HasDiscriminator<string>("UserType")
                  .HasValue<User>("User")
                  .HasValue<Customer>("Customer")
                  .HasValue<Staff>("Staff");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(c => c.Balance).HasDefaultValue(0);
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasOne<StaffType>()
                  .WithMany()
                  .HasForeignKey(s => s.TypeId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<StaffType>(entity =>
        {
            entity.HasKey(st => st.Id);
            entity.Property(st => st.Name).IsRequired();
        });

        // Product Related //
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.ProductName).IsRequired();
            entity.Property(p => p.Price).HasColumnType("decimal(18,2)");
            entity.Property(p => p.DateAdded).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.HasOne<Brand>()
                  .WithMany()
                  .HasForeignKey(p => p.BrandId)
                  .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne<Category>()
                  .WithMany()
                  .HasForeignKey(p => p.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(b => b.BrandName).IsRequired();
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.CategoryName).IsRequired();
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Code).IsRequired();
            entity.Property(d => d.Percentage).HasColumnType("decimal(5,2)");
            entity.Property(d => d.MinCost).HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Rating).IsRequired();
            entity.Property(r => r.Date).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.HasOne<Product>()
                  .WithMany()
                  .HasForeignKey(r => r.ProductId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne<Customer>()
                  .WithMany()
                  .HasForeignKey(r => r.CustomerId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Order Related //
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.HasOne<Customer>()
                  .WithMany()
                  .HasForeignKey(c => c.CustomerId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(ci => ci.Id);
            entity.Property(ci => ci.Quantity).IsRequired();
            entity.HasOne<Cart>()
                  .WithMany(c => c.Items)
                  .HasForeignKey(ci => ci.CartId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne<Product>()
                  .WithMany()
                  .HasForeignKey(ci => ci.ProductId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(o => o.Id);
            entity.Property(o => o.OrderDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(o => o.TotalAmount).HasColumnType("decimal(18,2)");
            entity.HasOne<Customer>()
                  .WithMany()
                  .HasForeignKey(o => o.CustomerId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Amount).HasColumnType("decimal(18,2)");
            entity.Property(p => p.PaymentDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.HasOne<Order>()
                  .WithMany()
                  .HasForeignKey(p => p.OrderId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Preference Related //
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.AddressName).IsRequired();
            entity.HasOne<Customer>()
                  .WithMany()
                  .HasForeignKey(a => a.CustomerId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Wishlist>(entity =>
        {
            entity.HasKey(w => w.Id);
            entity.Property(w => w.DateAdded).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.HasOne<Customer>()
                  .WithMany()
                  .HasForeignKey(w => w.CustomerId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(w => w.Items)
                  .WithMany();
        });

        // Authorization Related //
        modelBuilder.Entity<OperationClaim>(entity =>
        {
            entity.HasKey(oc => oc.Id);
            entity.Property(oc => oc.Name).IsRequired();
        });

        modelBuilder.Entity<UserOperationClaim>(entity =>
        {
            entity.HasKey(uoc => uoc.Id);
            entity.HasOne<User>()
                  .WithMany()
                  .HasForeignKey(uoc => uoc.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne<OperationClaim>()
                  .WithMany()
                  .HasForeignKey(uoc => uoc.OperationClaimId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Wishlist> Wishlists { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Staff> Staffes { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<StaffType> StaffTypes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
}