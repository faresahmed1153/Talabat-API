namespace Talabat.APIs.Core.Specifications
{
    public class ProductSpecParams
    {
        private const int maxPageSize = 10;
        private int pageSize = 5;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > maxPageSize ? maxPageSize : value; }
        }
        public int PageIndex { get; set; } = 1;

        public string? Sort { get; set; }

        public int? BrandId { get; set; }

        public int? TypeId { get; set; }


        private string search;

        public string? Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }

    }
}
