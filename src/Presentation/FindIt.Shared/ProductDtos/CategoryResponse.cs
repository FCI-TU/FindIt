namespace FindIt.Shared.DTOs;
public class CategoryResponse
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public List<ProductResponse> Products { get; set; } = new List<ProductResponse>();
}