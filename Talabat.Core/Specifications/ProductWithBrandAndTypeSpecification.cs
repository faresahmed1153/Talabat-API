using Talabat.APIs.Core.Specifications;
using Talabat.Core.Models;

namespace Talabat.Core.Specifications
{
    public class ProductWithBrandAndTypeSpecification : BaseSpecification<Product>
    {
        public ProductWithBrandAndTypeSpecification(ProductSpecParams productParams)
            : base(P =>

            (string.IsNullOrEmpty(productParams.Search) || P.Name.ToLower().Contains(productParams.Search)) &&
            (!productParams.BrandId.HasValue || P.ProductBrandId == productParams.BrandId.Value) &&
            (!productParams.TypeId.HasValue || P.ProductTypeId == productParams.TypeId.Value)
            )

        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);
            //if (productParams.Sort is null)
            //    AddOrderBy(_ => _);
            if (!string.IsNullOrEmpty(productParams.Sort))
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDecending(P => P.Price);
                        break;
                    case "nameDesc":
                        AddOrderByDecending(P => P.Name);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }

            ApplyPagination(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);
        }
        public ProductWithBrandAndTypeSpecification(int id) : base(P => P.Id == id)
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);
        }
    }
}
