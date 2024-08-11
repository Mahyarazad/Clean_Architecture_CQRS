using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NadinSoft.Domain.Entities.Product;


namespace NadinSoft.Persistence.Configurtions
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(128);
            builder.Property(p => p.ManufactureEmail).HasMaxLength(128);
            builder.Property(p => p.ManufacturePhone).HasMaxLength(128);

            builder.HasIndex(p => new { p.ManufactureEmail, p.ProductDate }, "UniqueIndex_MEmail_PDate").IsUnique(true);
        }
    }
}
