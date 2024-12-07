
using FindIt.Domain.Common;

namespace FindIt.Domain.ProductEntities
{
    public class Color : BaseEntity
    {
        public string ColorName { get; set; } = null!;
        public string HexCode { get; set; } = null!;

        public virtual ICollection<Product>? Products { get; set; }

    }
}
