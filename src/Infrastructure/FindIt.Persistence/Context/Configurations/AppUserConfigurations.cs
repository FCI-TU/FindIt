﻿using FindIt.Domain.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FindIt.Persistence.Context.Configurations
{
    internal class AppUserConfigurations : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            // Primary Key
            builder.HasKey(u => u.Id);

            // Property Configurations
            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(50); // Same as .HasColumnType("nvarchar(50)");

            builder.Property(u => u.LastName)
               .IsRequired()
               .HasMaxLength(50);

            // Relationships
            builder.HasMany(u => u.UserAddresses)
                .WithOne(add=>add.AppUser)
                .HasForeignKey(add => add.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.IdentityCodes)
                   .WithOne(ic => ic.AppUser)
                   .HasForeignKey(ic => ic.UserId)
                   .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(u => u.RefreshTokens)
                   .WithOne(rt => rt.AppUser)
                   .HasForeignKey(rt => rt.UserId)
                   .OnDelete(DeleteBehavior.Cascade);



        }
    }
}
