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
        //optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ReCapProjectDb;Trusted_Connection=true");
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