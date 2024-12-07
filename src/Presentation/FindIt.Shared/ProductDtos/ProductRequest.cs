using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace FindIt.Shared.DTOs;
public class ProductRequest
{
	[Required(ErrorMessage = "Product name is required")]
	[StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters")]
	public string Name { get; set; } = null!;

	[Required(ErrorMessage = "Description is required")]
	[StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
	public string Description { get; set; } = null!;

	[Required(ErrorMessage ="Image file is required")]
	public IFormFile Image { get; set; } = null!;

	[Required(ErrorMessage = "Quantity is required")]
	public decimal Quantity { get; set; }

	[Required(ErrorMessage = "Ratings Average is required")]
	public decimal RatingsAverage { get; set; }

	[Required(ErrorMessage = "Price is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Brand ID is required")]
    public int BrandId { get; set; }

    [Required(ErrorMessage = "Category ID is required")]
    public int CategoryId { get; set; }

	[Required(ErrorMessage ="Product Sizes is required")]
	public List<int> ProductSizes { get; set; } = new List<int>();
	[Required(ErrorMessage ="Color ID is required")]
	public int ColorId { get; set; }
}