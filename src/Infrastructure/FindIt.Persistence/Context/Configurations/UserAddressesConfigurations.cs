using FindIt.Domain.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FindIt.Persistence.Context.Configurations;
public class UserAddressesConfigurations : IEntityTypeConfiguration<UserAddress>
{
    public void Configure(EntityTypeBuilder<UserAddress> builder)
    {
        // Primary Key
        builder.HasKey(ua => ua.Id); 

        // Property Configurations
        builder.Property(ua => ua.AddressName)
               .HasMaxLength(200)
               .IsRequired();

        builder.Property(ua => ua.AddressLine)
               .HasMaxLength(300)
               .IsRequired();

        builder.Property(ua => ua.City)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(ua => ua.PostalCode)
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(ua => ua.Governate)
               .HasMaxLength(100);

        builder.Property(ua => ua.Country)
               .HasMaxLength(100)
               .IsRequired();

        // Relationship With AppUser
        builder.HasOne(ua => ua.AppUser)
               .WithMany(u => u.UserAddresses)
               .HasForeignKey(ua => ua.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
