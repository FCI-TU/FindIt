using AutoMapper;
using FindIt.Application.ErrorHandling;
using FindIt.Application.Interfaces;
using FindIt.Domain.Interfaces;
using FindIt.Domain.ProductEntities;
using FindIt.Domain.Specifications;
using FindIt.Shared.Authentications;
using FindIt.Shared.DTOs;

namespace FindIt.Application.Services
{
    public class BrandService : IBrandService

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISpecifications<Brand> _spec;

        public BrandService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
           
        }

        public async Task<Result<BrandResponse>> CreateBrandAsync(BrandRequest brandRequest)
        {
            // Map BrandRequest to Brand entity
            var brand = _mapper.Map<Brand>(brandRequest);

            // Create the brand in the database
            await _unitOfWork.Repository<Brand>().AddAsync(brand);
            await _unitOfWork.CompleteAsync();

            // Map created Brand to BrandResponse
            var brandResponse = _mapper.Map<BrandResponse>(brand);

            return Result<BrandResponse>.Success(brandResponse);
        }

        public async Task<Result<BrandResponse>> DeleteBrandAsync(int brandId)
        {
            var brand = await _unitOfWork.Repository<Brand>().GetByIdAsync(brandId);

            if (brand == null)
            {
                return Result.Failure<BrandResponse>(new Status(400, "Brand not found"));
            }

            _unitOfWork.Repository<Brand>().Delete(brand);
            await _unitOfWork.CompleteAsync();

            var brandResponse = _mapper.Map<BrandResponse>(brand);
            return Result<BrandResponse>.Success(brandResponse);
        }

        public async Task<Result<IReadOnlyList<BrandResponse>>> GetAllBrandsAsync()
        {
            var allBrands = await _unitOfWork.Repository<Brand>().GetAllAsync();

            var brandResponses = _mapper.Map<IReadOnlyList<BrandResponse>>(allBrands);

            return Result<IReadOnlyList<BrandResponse>>.Success(brandResponses);
        }

        public async Task<Result<BrandResponse>> GetBrandAsync(int brandId)
        {
            var brand = await _unitOfWork.Repository<Brand>().GetByIdAsync(brandId);

            if (brand == null)
            {
                return Result.Failure<BrandResponse>(new Status(400, "Brand not found"));
            }

            var brandResponse = _mapper.Map<BrandResponse>(brand);
            return Result<BrandResponse>.Success(brandResponse);
        }

        public async Task<Result<IReadOnlyList<BrandResponse>>> SearchBrandsAsync(string search)
        {
            // Create the specification for the search
            var searchSpec = new BaseSpecifications<Brand>();
            searchSpec.WhereCriteria= (b=> b.Name.Contains(search, StringComparison.OrdinalIgnoreCase));

            // Use the specification to get matching brands
            var brands = await _unitOfWork.Repository<Brand>().GetAllBySpecificationAsync(searchSpec);

            // Check if any brands were found
            if (!brands.Any())
            {
                return Result.Failure<IReadOnlyList<BrandResponse>>(new Status(400, "Brand not found"));
            }

            // Map the brands to the response DTO
            var brandResponses = _mapper.Map<IReadOnlyList<BrandResponse>>(brands);

            // Return the result
            return Result<IReadOnlyList<BrandResponse>>.Success(brandResponses);
        }


        public async Task<Result<BrandResponse>> UpdateBrandAsync(int brandId, BrandRequest brandRequest)
        {
            var brand = await _unitOfWork.Repository<Brand>().GetByIdAsync(brandId);

            if (brand == null)
            {
                return Result.Failure<BrandResponse>(new Status(400, "Brand not found"));
            }

            // Map changes from BrandRequest to the existing Brand
            _mapper.Map(brandRequest, brand);

            _unitOfWork.Repository<Brand>().Update(brand);
            await _unitOfWork.CompleteAsync();

            var brandResponse = _mapper.Map<BrandResponse>(brand);

            return Result<BrandResponse>.Success(brandResponse);
        }
    }
}
