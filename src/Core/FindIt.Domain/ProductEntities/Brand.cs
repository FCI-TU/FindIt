using FindIt.Domain.Common;
namespace FindIt.Domain.ProductEntities;
public class Brand : BaseEntity
{
    public string Name { get; set; } = null!;
    public string ImageCover { get; set; } = null!;
}