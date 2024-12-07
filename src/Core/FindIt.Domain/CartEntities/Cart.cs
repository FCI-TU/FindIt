using FindIt.Domain.Common;
using FindIt.Domain.IdentityEntities;

namespace FindIt.Domain.CartEntities
{
    public class Cart : BaseEntity
    {
        public string UserEmail { get; set; } = string.Empty;
        public int? DeliveryMethodId { get; set; }
        public decimal? ShippingPrice { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        
        // Navigation property to the CartItems
        //public virtual ICollection<CartItem> CartItems { get; set; } = null!;
    }
}
