namespace FindIt.Domain.Specifications.ProductSpecifications
{
    public class ProductSpecifications
    {
        private const int MaxPageSize = 10;
        private int _PageSize = 10;

        public int PageIndex = 1; 
        public int PageSize
        {
            get => _PageSize;
            set => _PageSize = value > MaxPageSize ? _PageSize : value;
        }

        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public string? Sort { get; set; }
        public string? Search {  get; set; }
    }
}
