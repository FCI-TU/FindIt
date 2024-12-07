using FindIt.Domain.Common;

namespace FindIt.Domain.ProductEntities;
public class Product : BaseEntity
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public string ImageCover { get; set; } = null!;

    public decimal Quantity { get; set; }

    public decimal RatingsAverage { get; set; }

    public bool Featured { get; set; }
    
    public int BrandId { get; set; }

    public Brand Brand { get; set; } = null!;

    public int CategoryId { get; set; }

    public Category Category { get; set; } = null!;

    public int ColorId { get; set; }
    
    public Color Color { get; set; } = null!;

    public virtual ICollection<ProductSize> ProductSizes { get; set; } = null!;
}