using FindIt.Domain.Common;

namespace FindIt.Domain.IdentityEntities;
public class UserAddress : BaseEntity
{
    public string AddressName { get; set; } = null!;
    public string AddressLine { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string Governate { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public required AppUser AppUser { get; set; }
}