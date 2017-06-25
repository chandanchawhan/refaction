using refactor_me.Core.Domain.Models;
using System.Data.Entity.ModelConfiguration;

namespace refactor_me.Persistence.EntityConfigurations
{
    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            Property(po => po.Id)
                .IsRequired();

            Property(po => po.Price)
                .IsRequired();

            Property(po => po.DeliveryPrice)
                .IsRequired();

            Property(p => p.Description)
                .HasMaxLength(500);

        }
    }
}