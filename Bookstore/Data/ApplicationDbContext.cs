using Bookstore.Entities;
using Bookstore.Model;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }
    
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();
        
        // Generic: Apply default timestamp for CreatedDate if exists
        foreach (var entityType in modelBuilder.Model.GetEntityTypes()) {
            var property = entityType.ClrType.GetProperty("CreatedDate");
            if (property != null && property.PropertyType == typeof(DateTime))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property("CreatedDate")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            }
        }
        
        modelBuilder.Entity<Order>()
            .Property(o => o.PaymentMethod)
            .HasConversion<string>()
            .HasMaxLength(20);
    }
}