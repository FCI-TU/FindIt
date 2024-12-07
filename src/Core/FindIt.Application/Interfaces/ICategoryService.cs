using FindIt.Application.ErrorHandling;
using FindIt.Shared.DTOs;

namespace FindIt.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<Result<CategoryRequest>> CreateCategoryAsync(CategoryRequest request);
        Task<Result<CategoryResponse>> GetCategoryByIdAsync(int id);
        Task<Result<IReadOnlyList<CategoryResponse>>> GetAllCategoriesAsync();
        Task<Result<IReadOnlyList<CategoryResponse>>> SearchAsync(string searchQuery);
        Task<Result<CategoryResponse>> UpdateCategoryAsync(int id, CategoryRequest request);
        Task<Result<string>> DeleteCategoryAsync(int id);
    }
}
