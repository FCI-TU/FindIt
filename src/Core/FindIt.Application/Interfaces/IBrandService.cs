
using FindIt.Application.ErrorHandling;
using FindIt.Shared.DTOs;

namespace FindIt.Application.Interfaces
{
    public interface IBrandService
    {
        Task<Result<IReadOnlyList<BrandResponse>>> GetAllBrandsAsync();
        Task<Result<IReadOnlyList<BrandResponse>>> SearchBrandsAsync(string search);
        Task<Result<BrandResponse>> GetBrandAsync(int brandId);
        Task<Result<BrandResponse>> CreateBrandAsync(BrandRequest brandRequest);
        Task<Result<BrandResponse>> UpdateBrandAsync(int brandId, BrandRequest brandRequest);
        Task<Result<BrandResponse>> DeleteBrandAsync(int brandId);
    }
}
