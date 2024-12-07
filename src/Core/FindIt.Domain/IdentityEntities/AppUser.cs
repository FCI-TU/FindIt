using Microsoft.AspNetCore.Identity;

namespace FindIt.Domain.IdentityEntities;
public class AppUser : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public virtual ICollection<UserAddress>? UserAddresses { get; set; }
    public virtual ICollection<IdentityCode> IdentityCodes { get; set; } = null!;
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = null!;
}