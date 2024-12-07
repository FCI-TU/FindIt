using System.ComponentModel.DataAnnotations;

namespace FindIt.Shared.DTOs;
public class BrandRequest
{
	[Required(ErrorMessage = "Brand name is required")]
	[StringLength(100, ErrorMessage = "Brand name cannot exceed 100 characters")]
	public string Name { get; set; } = null!;

	[Required(ErrorMessage = "Image Cover is required")]
	[StringLength(500, ErrorMessage = "Image Cover cannot exceed 500 characters")]
	public string ImageCover { get; set; } = null!;
}