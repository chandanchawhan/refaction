using refactor_me.Core.Domain.Models;
using refactor_me.Persistence.EntityConfigurations;
using System.Data.Entity;

namespace refactor_me.Persistence
{
    public class ProductApplicationContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOption> ProductOptions { get; set; }

        public ProductApplicationContext()
            : base("name=ProductApplicationContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductConfiguration());
            modelBuilder.Configurations.Add(new ProductOptionConfiguration());

            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductOptions)
                .WithRequired(po => po.Product)
                .HasForeignKey(po => po.ProductId)
                .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }
    }
}