using FindIt.Application.ErrorHandling;
using FindIt.Application.Interfaces;
using FindIt.Application.Specifications.CategorySpecifications;
using FindIt.Domain.Interfaces;
using FindIt.Domain.ProductEntities;
using FindIt.Domain.Specifications;
using FindIt.Shared.DTOs;
using FindIt.Shared.ProductDtos;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindIt.Application.Services;

public class CategoryService(IUnitOfWork unitOfWork) : ICategoryService
{
	public async Task<Result<CategoryResponse>> CreateCategoryAsync(CategoryRequest request)
	{
		await unitOfWork.Repository<Category>().AddAsync(new Category { Name = request.Name });
		await unitOfWork.CompleteAsync();

		// var category = await unitOfWork.Repository<Category>().GetByCriteriaAsync(new CategoryWithAllRelatedData { Search = request.Name });
		// return Result<CategoryResponse>.Success(new CategoryResponse { Id = category.Id, Name = category.Name, Products = category.Products });
		throw new NotImplementedException();
	}

	public async Task<Result<string>> DeleteCategoryAsync(int id)
	{
		var category = await unitOfWork.Repository<Category>().GetByIdAsync(id);
		if (category == null)
		{
			return Result.Failure<string>(new Status(StatusCode.NotFound, "Category not found"));
		}
		unitOfWork.Repository<Category>().Delete(category);
		await unitOfWork.CompleteAsync();
		return Result<string>.Success("Category deleted successfully");
	}

	public async Task<Result<List<CategoryResponse>>> GetAllCategoriesAsync()
	{
		var categories = await unitOfWork.Repository<Category>()
			.GetAllAsync();
		return Result.Success(categories.Select(c => CategoryToCategoryResponse(c)).ToList());
	}

	public async Task<Result<CategoryResponse>> GetCategoryByIdAsync(int id)
	{
		var category = await unitOfWork.Repository<Category>().GetByIdAsync(id);
		if (category == null)
		{
			return Result.Failure<CategoryResponse>(new Status(StatusCode.NotFound, "Category not found"));
		}
		unitOfWork.Repository<Category>().Delete(category);
		await unitOfWork.CompleteAsync();

		return Result.Success(CategoryToCategoryResponse(category));
	}

    public async Task<Result<IReadOnlyList<CategoryResponse>>> SearchAsync(string searchQuery)
	{
		throw new NotImplementedException();
	}

	public async Task<Result<CategoryResponse>> UpdateCategoryAsync(int id, CategoryRequest request)
	{
		var category = await unitOfWork.Repository<Category>().GetByIdAsync(id);
		if (category == null)
		{
			return Result.Failure <CategoryResponse>(new Status(StatusCode.NotFound, "Category not found"));
		}
		unitOfWork.Repository<Category>().Update(new Category { Name = request.Name });
		await unitOfWork.CompleteAsync();
		return Result.Success(new CategoryResponse { Name = category.Name });
	}

    private CategoryResponse CategoryToCategoryResponse(Category category)
    {
        var productResponses = category.Products.Select(p => new ProductResponse {
			Id = p.Id,
			Name = p.Name,
			Price = p.Price,
			Quantity = p.Quantity,
			Description = p.Description,
			ImageCover = p.ImageCover,
			RatingsAverage = p.RatingsAverage,
			BrandName = p.Brand.Name,
			CategoryName = p.Category.Name,
			Color = new ColorResponse { ColorName = p.Color.ColorName, HexCode = p.Color.HexCode },
			Sizes = p.ProductSizes.Select(s => new ProductSizeResponse { SizeId = s.SizeId, SizeName = s.Size.SizeName }).ToList(),
		}).ToList();

		return new CategoryResponse { Id = category.Id, Name = category.Name, Products = productResponses };
    }
}
