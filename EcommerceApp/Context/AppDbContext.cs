using EcommerceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ProductModel> Products { get; set; }
        public DbSet<CategoryModel> Categorys { get; set; }

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
        }
    }
}
