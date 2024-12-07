using FindIt.Domain.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FindIt.Persistence.Context.Configurations;
public class IdentityCodesConfigurations : IEntityTypeConfiguration<IdentityCode>
{
    public void Configure(EntityTypeBuilder<IdentityCode> builder)
    {
        // Primary Key
        builder.HasKey(ic => ic.Id); 

        // Property Configurations
        builder.Property(ic => ic.Code)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(ic => ic.IsActive)
               .IsRequired();

        builder.Property(ic => ic.ForRegistrationConfirmation)
               .IsRequired();

        builder.Property(ic => ic.CreationTime)
               .IsRequired();

        builder.Property(ic => ic.ActivationTime)
               .IsRequired(false); // Maybe Null

        // Relationship With AppUser
        builder.HasOne(ic => ic.AppUser)
               .WithMany(u => u.IdentityCodes)
               .HasForeignKey(ic => ic.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}