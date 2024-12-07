using System.ComponentModel.DataAnnotations;

namespace FindIt.Shared.DTOs;
public class CategoryRequest
{
	[Required(ErrorMessage = "Category name is required")]
	[StringLength(100, ErrorMessage = "Category name cannot exceed 100 characters")]
	public string Name { get; set; } = null!;
}