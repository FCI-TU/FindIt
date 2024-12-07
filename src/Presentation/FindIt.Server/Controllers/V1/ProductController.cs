using Asp.Versioning;
using FindIt.Application.Interfaces;
using FindIt.Domain.Specifications.ProductSpecifications;
using FindIt.Server.Extensions;
using FindIt.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FindIt.Server.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetAllProducts([FromQuery]ProductSpecifications specifications)
        {
            var result = await productService.GetAllProductsAsync(specifications);

            return result.IsSuccess ? result.ToSuccess(result.Value) : result.ToProblem();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetProduct(int id)
        {
            var result = await productService.GetProductAsync(id);
            return result.IsSuccess ? result.ToSuccess(result.Value) : result.ToProblem();

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, [FromForm]ProductRequest productRequest)
        {
            var result = await productService.UpdateProductAsync(id, productRequest);
            return result.IsSuccess ? result.ToSuccess(result.Value) : result.ToProblem();

        }
        [HttpPost]
        public async Task<ActionResult> CreateProduct([FromForm]ProductRequest productRequest)
        {
            var result = await productService.CreateProductAsync(productRequest);
            return result.IsSuccess ? result.ToSuccess(result.Value) : result.ToProblem();

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var result = await productService.DeleteProductAsync(id);
            return result.IsSuccess ? result.ToSuccess(result.Value) : result.ToProblem();

        }

    }
}
