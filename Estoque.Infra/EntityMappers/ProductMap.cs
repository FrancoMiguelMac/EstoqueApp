using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Estoque.Domain.Entities;

namespace Estoque.Infra.EntityMappers
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product", "sales");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(40).IsRequired();

            builder.Ignore(x => x.ValidationResult);
        }
    }
}
