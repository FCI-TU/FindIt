

using FindIt.Application.DTOs;
using FindIt.Application.ErrorHandling;
using FindIt.Domain.Specifications.ProductSpecifications;
using FindIt.Shared.DTOs;

namespace FindIt.Application.Interfaces
{
    public interface IProductService
    {
        Task<Result<Pagination<ProductResponse>>> GetAllProductsAsync(ProductSpecifications productSpecifications);
        Task<Result<ProductResponse>> GetProductAsync(int productId);
        Task<Result<ProductResponse>> CreateProductAsync(ProductRequest productRequest);
        Task<Result<ProductResponse>> UpdateProductAsync(int productId, ProductRequest productRequest);
        Task<Result<string>> DeleteProductAsync(int productId);
    }
}
