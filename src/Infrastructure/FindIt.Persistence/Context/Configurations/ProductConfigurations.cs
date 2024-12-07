using FindIt.Domain.ProductEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FindIt.Persistence.Context.Configurations;
internal class ProductConfigurations : IEntityTypeConfiguration<Product>
{
	public void Configure(EntityTypeBuilder<Product> builder)
	{
		builder.Property(p => p.Name)
			.IsRequired()
			.HasMaxLength(100);

		builder.Property(p => p.Description)
			.IsRequired()
			.HasMaxLength(255);

		builder.Property(p => p.Price)
			.IsRequired()
			.HasColumnType("decimal(18,2)");

		builder.Property(p => p.ImageCover)
			.IsRequired()
			.HasMaxLength(300);

		builder.Property(p => p.Quantity)
			.IsRequired()
			.HasColumnType("decimal(18,2)");

		builder.Property(p => p.RatingsAverage)
			.IsRequired()
			.HasColumnType("decimal(18,2)");
	}
}