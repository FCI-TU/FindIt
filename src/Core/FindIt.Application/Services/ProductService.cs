using FindIt.Application.DTOs;
using FindIt.Application.ErrorHandling;
using FindIt.Application.Interfaces;
using FindIt.Domain.Interfaces;
using FindIt.Domain.ProductEntities;
using FindIt.Domain.Specifications.ProductSpecifications;
using FindIt.Shared.DTOs;
using FindIt.Shared.ProductDtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FindIt.Application.Services
{
    public class ProductService(IUnitOfWork unitOfWork, IWebHostEnvironment environment) : IProductService
    {

        public async Task<Result<Pagination<ProductResponse>>> GetAllProductsAsync(ProductSpecifications productSpecifications)
        {
            var specifications = new ProductWithAllRelatedData(productSpecifications); 

            var products = await unitOfWork.Repository<Product>().GetAllBySpecificationAsync(specifications);

            // map products list to Readonly list of product response manually. 
            List<ProductResponse> productsDto = new List<ProductResponse>();
            foreach (var product in products)
            {
                productsDto.Add(new ProductResponse
                {
                    Id = product.Id,
                    Name = product.Name,
                    BrandId = product.BrandId,
                    BrandName = product.Brand.Name,
                    CategoryId = product.CategoryId,
                    CategoryName = product.Category.Name,
                    Price = product.Price,
                    RatingsAverage = product.RatingsAverage,
                    Color = new ColorResponse { ColorName = product.Color.ColorName, HexCode = product.Color.HexCode },
                    ImageCover = product.ImageCover,
                    Description = product.Description,
                    Quantity = product.Quantity,
                    Sizes = (product.ProductSizes
                    .Select(x => new ProductSizeResponse { SizeId = x.SizeId, SizeName = x.Size.SizeName })).ToList(),
                });
            }

            var productWithPagination = new Pagination<ProductResponse>
                (productSpecifications.PageSize, productSpecifications.PageSize, productsDto.Count, productsDto);

            return Result.Success(productWithPagination);
        }

        public async Task<Result<ProductResponse>> GetProductAsync(int productId)
        {
            var specifications = new ProductWithAllRelatedData(productId);

            var productToReturn = await unitOfWork.Repository<Product>().GetByCriteriaAsync(specifications);

            if (productToReturn is null)
                return Result.Failure<ProductResponse>
                    (new Status(404, "The product you are looking for does not exists. Please check the product ID and try again."));

            var productDto = new ProductResponse()
            {
                Id = productToReturn.Id,
                Price = productToReturn.Price,
                Name = productToReturn.Name,
                Quantity = productToReturn.Quantity,
                RatingsAverage = productToReturn.RatingsAverage,
                Description = productToReturn.Description,
                ImageCover = productToReturn.ImageCover,
                BrandId = productToReturn.BrandId,
                CategoryId = productToReturn.CategoryId,
                BrandName = productToReturn.Brand.Name,
                CategoryName = productToReturn.Category.Name,
                Color = new ColorResponse { ColorName = productToReturn.Color.ColorName, HexCode = productToReturn.Color.HexCode },
                Sizes = (productToReturn.ProductSizes.Select(x => new ProductSizeResponse { SizeId = x.SizeId, SizeName = x.Size.SizeName })).ToList(),
            };

            return Result.Success<ProductResponse>(productDto);
        }

        public async Task<Result<ProductResponse>> CreateProductAsync(ProductRequest productRequest)
        {
            #region Validations 
            var category = await unitOfWork.Repository<Category>().GetByIdAsync(productRequest.CategoryId);
            if (category == null)
                return Result.Failure<ProductResponse>(new Status(404, $"The category with id {productRequest.CategoryId} does not exists. Please check category ID and try again."));

            var brand = await unitOfWork.Repository<Brand>().GetByIdAsync(productRequest.BrandId);
            if (brand == null)
                return Result.Failure<ProductResponse>(new Status(404, $"The brand with id {productRequest.BrandId} does not exists. Please check brand ID and try again."));

            var color = await unitOfWork.Repository<Color>().GetByIdAsync(productRequest.ColorId);
            if (color == null)
                return Result.Failure<ProductResponse>(new Status(404, $"The color with id {productRequest.BrandId} does not exists. Please check color ID and try again."));

            if (productRequest.ProductSizes is null || !productRequest.ProductSizes.Any())
                return Result.Failure<ProductResponse>(new Status(400, "Product sizes cannot be empty. Please enter product sizes."));
            #endregion


            var product = new Product()
            {
                Name = productRequest.Name,
                Price = productRequest.Price,
                Quantity = productRequest.Quantity,
                Description = productRequest.Description,
                BrandId = productRequest.BrandId,
                Category = category,
                Color = color,
                Brand = brand,
                CategoryId = productRequest.CategoryId,
                ColorId = productRequest.ColorId,
                RatingsAverage = productRequest.RatingsAverage,
                ImageCover = "default.png" // for avoiding null.

            };

            await unitOfWork.Repository<Product>().AddAsync(product);
            var result = await unitOfWork.CompleteAsync();
            if (result <= 0)
                return Result.Failure<ProductResponse>(new Status(500, "An error occurs while saving product."));

            var productSizes = new List<ProductSize>();
            foreach(var sizeId in productRequest.ProductSizes)
            {
                var size = await unitOfWork.Repository<Size>().GetByIdAsync(sizeId);
                productSizes.Add(new ProductSize()
                {
                    ProductId = product.Id,
                    SizeId = sizeId,
                    Size = size!
                });

            }

            var imageUrl = await SaveImageAsync(productRequest.Image, product.Id);

            product.ImageCover = imageUrl;


            // add product sizes 
            await unitOfWork.Repository<ProductSize>().AddRangeAsync(productSizes);

            product.ProductSizes = productSizes;

            unitOfWork.Repository<Product>().Update(product);

            // save changes to database

            result = await unitOfWork.CompleteAsync();

            if (result <= 0)
                return Result.Failure<ProductResponse>(new Status(500, "An error occurs while saving product sizes."));


            // manual mapping (product to product DTO)
            var productResponse = new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Quantity = product.Quantity,
                RatingsAverage = product.RatingsAverage,
                BrandName = brand.Name,
                CategoryName = category.Name,
                BrandId = product.BrandId,
                CategoryId = category.Id,
                Color = new ColorResponse { ColorName = color.ColorName, HexCode = color.HexCode },
                ImageCover = product.ImageCover,
                Sizes = (product.ProductSizes.Select(ps => new ProductSizeResponse { SizeId = ps.SizeId, SizeName = ps.Size.SizeName })).ToList()
            };


            return Result.Success(productResponse);

        }

        public async Task<Result<string>> DeleteProductAsync(int productId)
        {
            var productToDelete = await unitOfWork.Repository<Product>().GetByIdAsync(productId);

            if (productToDelete is null)
                return Result.Failure<string>(new Status(404, "The product you are looking for does not exists. Please check the product ID and try again."));

            /// delete image service calling. 

            var isImageDeleted = await DeleteImageAsync(productToDelete.ImageCover);
            
            unitOfWork.Repository<Product>().Delete(productToDelete);
            var result = await unitOfWork.CompleteAsync();
            return result <= 0
                ? Result.Failure<string>(new Status(500, "An error occured while deleting a product. Please try again.")) :
                Result.Success<string>("Product deleted successfully.");

        }

        public async Task<Result<ProductResponse>> UpdateProductAsync(int productId, ProductRequest productRequest)
        {
            var productSpecifications = new ProductWithAllRelatedData(productId);
            var existingProduct = await unitOfWork.Repository<Product>().GetByCriteriaAsync(productSpecifications);
            if (existingProduct is null)
                return Result.Failure<ProductResponse>(new Status(404, "The product with ID does not exist. Please check the product ID and try again."));

            // Handle category update
            var category = await unitOfWork.Repository<Category>().GetByIdAsync(productRequest.CategoryId);
            if (category is null) return Result.Failure<ProductResponse>(new Status(404, "Category not found."));
            existingProduct.Category = category;

            // Handle color update
            var color = await unitOfWork.Repository<Color>().GetByIdAsync(productRequest.ColorId);
            if (color != null) existingProduct.Color = color;

            existingProduct.Quantity = productRequest.Quantity;
            existingProduct.Description = productRequest.Description;
            existingProduct.Price = productRequest.Price;
            existingProduct.BrandId = productRequest.BrandId;
            existingProduct.CategoryId = productRequest.CategoryId;
            existingProduct.ColorId = productRequest.ColorId;
            existingProduct.Name = productRequest.Name;
            existingProduct.RatingsAverage = productRequest.RatingsAverage;

            // Update product sizes 
            existingProduct.ProductSizes.Clear();
            foreach (var sizeId in productRequest.ProductSizes)
            {
                var size = await unitOfWork.Repository<Size>().GetByIdAsync(sizeId);
                existingProduct.ProductSizes.Add(new ProductSize
                {
                    ProductId = existingProduct.Id,
                    Size = size!, 
                    SizeId = sizeId
                });
            }

            
            // Delete old image from wwwroot and save new one.
            var newImageUrl = await SaveImageAsync(productRequest.Image, productId);
            existingProduct.ImageCover = newImageUrl;

            unitOfWork.Repository<Product>().Update(existingProduct);
            var result = await unitOfWork.CompleteAsync();

            if (result <= 0)
                return Result.Failure<ProductResponse>(new Status(500, "An error occurred while updating the product. Please try again."));

            var productResponse = new ProductResponse
            {
                Id = existingProduct.Id,
                Name = existingProduct.Name,
                Price = existingProduct.Price,
                Description = existingProduct.Description,
                Quantity = existingProduct.Quantity,
                RatingsAverage = existingProduct.RatingsAverage,
                BrandName = existingProduct.Brand.Name,
                CategoryName = existingProduct.Category.Name,
                BrandId = existingProduct.BrandId,
                CategoryId = existingProduct.Id,
                Color = new ColorResponse { ColorName = existingProduct.Color.ColorName, HexCode = existingProduct.Color.HexCode },
                ImageCover = existingProduct.ImageCover,
                Sizes = (existingProduct.ProductSizes.Select(ps => new ProductSizeResponse { SizeId = ps.SizeId, SizeName = ps.Size.SizeName })).ToList()
            };

            return Result.Success(productResponse);
        }

        private async Task<string> SaveImageAsync(IFormFile file, int productId)
        {
            if (file == null || file.Length == 0)
            {
                return "Invalid image file";
            }

          
            var imagePath = Path.Combine("Images", $"Product-{productId.ToString()}");
            var finalPath = Path.Combine(environment.WebRootPath, imagePath);
            if (!Directory.Exists(finalPath))
            {
                Directory.CreateDirectory(finalPath);
            }
            else
            {
                Directory.Delete(finalPath, true);
                Directory.CreateDirectory(finalPath);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(finalPath, fileName);

            while (File.Exists(filePath))
            {
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                filePath = Path.Combine(finalPath, fileName);
            }
            // save the file to the specific path
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var imageUrl = Path.Combine(imagePath, fileName).Replace("\\", "/");

            return imageUrl;
        }
        private async Task<bool> DeleteImageAsync(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                return false; // Invalid image URL
            }

            // Convert the image URL to a file path
            var filePath = Path.Combine(environment.WebRootPath, imageUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
            Console.WriteLine($"Attempting to delete: {filePath}");
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                    return true; // Image successfully deleted
                }
                catch (Exception ex)
                {
                    // Log the exception (logging not shown here)
                    return false; // Failed to delete the image
                }
            }



            return false; // File not found
        }
    }
}
