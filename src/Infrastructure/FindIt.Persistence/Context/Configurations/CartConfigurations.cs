using FindIt.Domain.IdentityEntities;
using FindIt.Domain.CartEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FindIt.Persistence.Context.Configurations
{
    public class CartConfigurations : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            // Primary Key
            builder.HasKey(c => c.Id);

            builder.Property(c => c.UserEmail)
                .IsRequired()
                .HasMaxLength(200);

            
            //// Relationship With CartItems (One-to-Many)
            //builder.HasMany(c => c.CartItems)
            //    .WithOne(ci => ci.Cart)
            //    .HasForeignKey(ci => ci.BasketId)
            //    .OnDelete(DeleteBehavior.Cascade); 

        }
    }
}
