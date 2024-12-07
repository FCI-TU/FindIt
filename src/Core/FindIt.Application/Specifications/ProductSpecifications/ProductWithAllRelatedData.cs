using FindIt.Domain.ProductEntities;
using Microsoft.EntityFrameworkCore;

namespace FindIt.Domain.Specifications.ProductSpecifications
{
    public class ProductWithAllRelatedData : BaseSpecifications<Product>
    {
        public ProductWithAllRelatedData(ProductSpecifications productSpecifications)
        {
            IncludesCriteria.Add(q=>q.Include(p=>p.Category));
            IncludesCriteria.Add(q => q.Include(p => p.Brand));
            IncludesCriteria.Add(q => q.Include(p => p.Color));
            IncludesCriteria.Add(q => q.Include(p => p.ProductSizes).ThenInclude(ps => ps.Size));

            AddWhere(p => 
                    (string.IsNullOrEmpty(productSpecifications.Search) ||
                     p.Name.ToLower().Contains(productSpecifications.Search.ToLower())) &&
                    (!productSpecifications.CategoryId.HasValue || p.CategoryId == productSpecifications.CategoryId.Value)
                    );

            if (!string.IsNullOrEmpty(productSpecifications.Sort))
            {
                switch (productSpecifications.Sort)
                {
                    case "name":
                        ApplyOrderBy(p => p.Name);
                        break;
                    case "nameDesc":
                        ApplyOrderByDescending(p => p.Name);
                        break;
                    case "price":
                        ApplyOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        ApplyOrderByDescending(p => p.Price);
                        break;
                    case "best":
                        ApplyOrderByDescending(p=>p.RatingsAverage);
                        break;
                    default:
                        ApplyOrderBy(p => p.Name);
                        break;
                }
            }
            else
            {
                ApplyOrderBy(p=>p.Price);
            }


            ApplyPagination((productSpecifications.PageIndex - 1) * productSpecifications.PageSize, productSpecifications.PageSize);

        }

        // Git single product with their related data, pagination does not needed here.
        public ProductWithAllRelatedData(int productId)
        {
            IncludesCriteria.Add(q => q.Include(p => p.Category));
            IncludesCriteria.Add(q => q.Include(p => p.Brand));
            IncludesCriteria.Add(q => q.Include(p => p.Color));
            IncludesCriteria.Add(q=>q.Include(p=>p.ProductSizes).ThenInclude(ps=>ps.Size));


        }
    }

}
