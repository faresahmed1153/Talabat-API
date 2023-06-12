using Talabat.APIs.Core.Specifications;
using Talabat.Core.Models;

namespace Talabat.Core.Specifications
{
    public class ProductWithFilterationForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFilterationForCountSpecification(ProductSpecParams productParams)
            : base(P =>
            (string.IsNullOrEmpty(productParams.Search) || P.Name.ToLower().Contains(productParams.Search)) &&
            (!productParams.BrandId.HasValue || P.ProductBrandId == productParams.BrandId.Value) &&
            (!productParams.TypeId.HasValue || P.ProductTypeId == productParams.TypeId.Value)
            )
        {

        }
    }
}
