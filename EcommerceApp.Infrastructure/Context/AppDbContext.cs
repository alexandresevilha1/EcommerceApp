using EcommerceApp.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Infrastructure.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ProductModel> Products { get; set; }
        public DbSet<CategoryModel> Categorys { get; set; }
        public DbSet<CartModel> Carts { get; set; }
        public DbSet<CartItemModel> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductModel>(entity =>
            {
                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(200);
                entity.Property(p => p.Price)
                .IsRequired();
            });

            modelBuilder.Entity<CategoryModel>(entity =>
            {
                entity.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(200);
            });

            modelBuilder.Entity<CartModel>()
                .HasOne(c => c.ApplicationUser)
                .WithOne()
                .HasForeignKey<CartModel>(c => c.ApplicationUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartItemModel>()
                .HasOne(ic => ic.Product)
                .WithMany()
                .HasForeignKey(ic => ic.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
