using FindIt.Application.ErrorHandling;
using FindIt.Application.Interfaces;
using FindIt.Server.Controllers;
using FindIt.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FindIt.Api.Controllers
{
    public class BrandController(IBrandService _brandService) : ApiBaseController
    {
        #region Create
        [HttpPost("Create")]
        public async Task<ActionResult<BrandResponse>> CreateBrandAsync([FromBody] BrandRequest? brandRequest)
        {
            if (brandRequest == null)
            {
                return BadRequest(new Status(400, "The brand request cannot be null. Please provide valid brand details."));
            }

            var brand = await _brandService.CreateBrandAsync(brandRequest);
            if (!brand.IsSuccess)
            {
                return BadRequest(new Status(400, "Failed to create the brand. Please ensure the provided details are correct."));
            }

            return Ok(brand);
        }
        #endregion
       
        #region Delete
        [HttpDelete("Delete/{brandId}")]
        public async Task<ActionResult<BrandResponse>> DeleteBrandAsync(int brandId)
        {
            var existingBrand = await _brandService.GetBrandAsync(brandId);

            if (existingBrand == null)
            {
                return BadRequest(new Status(400, "The brand with the specified ID does not exist. Please provide a valid ID."));
            }

            var result = await _brandService.DeleteBrandAsync(brandId);
            if (!result.IsSuccess)
            {
                return BadRequest(new Status(400, "Failed to delete the brand. Please try again."));
            }

            return Ok(result);
        }
        #endregion

        #region AllBrands
        [HttpGet("AllBrands")]
        public async Task<IActionResult> GetAllBrandsAsync()
        {
            var result = await _brandService.GetAllBrandsAsync();

            if (result == null)
            {
                return NotFound(new Status(404, "No brands were found."));
            }

            return Ok(result);
        }
        #endregion

        #region GetBrand
        [HttpGet("{brandId}")]
        public async Task<ActionResult<BrandResponse>> GetBrandAsync(int brandId)
        {
            var brand = await _brandService.GetBrandAsync(brandId);

            if (brand == null)
            {
                return NotFound(new Status(404, "The brand with the specified ID does not exist."));
            }

            return Ok(brand);
        }
        #endregion

        #region Search Brands
        [HttpGet("Search")]
        public async Task<ActionResult<IReadOnlyList<BrandResponse>>> SearchBrandsAsync([FromQuery] string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return BadRequest(new Status(400, "The search query cannot be empty. Please provide a valid search term."));
            }

            var brands = await _brandService.SearchBrandsAsync(search);
            if (brands == null)
            {
                return NotFound(new Status(404, "No brands matched the provided search criteria."));
            }

            return Ok(brands);
        }
        #endregion

        #region Update Brand
        [HttpPut("Update/{brandId}")]
        public async Task<ActionResult<BrandResponse>> UpdateBrandAsync(int brandId, [FromBody] BrandRequest? brandRequest)
        {
            if (brandRequest == null)
            {
                return BadRequest(new Status(400, "The brand request cannot be null. Please provide valid brand details."));
            }

            var result = await _brandService.UpdateBrandAsync(brandId, brandRequest);
            if (!result.IsSuccess)
            {
                return BadRequest(new Status(400, "Failed to update the brand. Please ensure the details are correct and try again."));
            }

            return Ok(result);
        } 
        #endregion
    }
}
