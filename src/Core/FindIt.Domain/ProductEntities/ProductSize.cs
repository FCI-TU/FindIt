using FindIt.Domain.Common;

namespace FindIt.Domain.ProductEntities
{
    public class ProductSize
    {
        public int SizeId { get; set; } 
        public Size Size { get; set; } = null!;
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}