using FindIt.Domain.Common;

namespace FindIt.Domain.IdentityEntities;
public class IdentityCode : BaseEntity
{
    public string Code { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool ForRegistrationConfirmation { get; set; }

    public DateTime CreationTime { get; set; }

    public DateTime? ActivationTime { get; set; }

    public string UserId { get; set; } = null!;

    public virtual AppUser AppUser { get; set; } = null!;
}