using FindIt.Domain.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FindIt.Persistence.Context.Configurations;
public class RefreshTokensConfigurations : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        // Primary Key
        builder.HasKey(rt => rt.Id);

        // Property Configurations
        builder.Property(rt => rt.Token)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(rt => rt.CreatedAt)
               .IsRequired();

        builder.Property(rt => rt.ExpiredAt)
               .IsRequired();

        builder.Property(rt => rt.RevokedAt)
               .IsRequired(false); // It might be null if the token is still active

        // Relationship With AppUser
        builder.HasOne(rt => rt.AppUser)
               .WithMany(u => u.RefreshTokens)
               .HasForeignKey(rt => rt.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}