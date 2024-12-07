using FindIt.Shared.ProductDtos;

namespace FindIt.Shared.DTOs;
public class ProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ImageCover { get; set; } = null!;
    public decimal Quantity { get; set; }
    public decimal RatingsAverage { get; set; }
    public decimal Price { get; set; }
    public int BrandId { get; set; }
    public string BrandName { get; set; } = string.Empty;
	public int CategoryId { get; set; }
	public string CategoryName { get; set; } = string.Empty;
    public List<ProductSizeResponse> Sizes { get; set; } = new List<ProductSizeResponse>();
    public ColorResponse Color { get; set; } = new(); 
}