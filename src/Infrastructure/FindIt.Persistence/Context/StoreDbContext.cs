using FindIt.Domain.CartEntities;
using FindIt.Domain.IdentityEntities;
using FindIt.Domain.ProductEntities;
using FindIt.Persistence.Context.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FindIt.Persistence.Context;
public class StoreDbContext(DbContextOptions<StoreDbContext> options) : IdentityDbContext<AppUser>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppUserConfigurations).Assembly);
    }

    public DbSet<IdentityCode> IdentityCodes { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<UserAddress> UserAddresses { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Cart> Carts { get; set; }

}