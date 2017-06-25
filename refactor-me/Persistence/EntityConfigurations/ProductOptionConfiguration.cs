using refactor_me.Core.Domain.Models;
using System.Data.Entity.ModelConfiguration;

namespace refactor_me.Persistence.EntityConfigurations
{
    public class ProductOptionConfiguration : EntityTypeConfiguration<ProductOption>
    {
        public ProductOptionConfiguration()
        {
            Property(po => po.Name)
                .HasMaxLength(100)
                .IsRequired();

            Property(po => po.Id)
                .IsRequired();

            Property(po => po.ProductId)
                .IsRequired();


            Property(po => po.Description)
                .HasMaxLength(500);
        }
    }
}