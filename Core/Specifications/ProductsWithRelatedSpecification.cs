using Core.Entities;

namespace Core.Specifications
{
	public class ProductsWithRelatedSpecification : BaseSpecification<Product>
	{
		public ProductsWithRelatedSpecification()
		{
			base.AddInclude(x => x.ProductBrand);
			base.AddInclude(x => x.ProductType);
			base.AddOrderBy(x => x.Name);
		}

		public ProductsWithRelatedSpecification(int id) : base(x => x.Id == id)
		{
			base.AddInclude(x => x.ProductBrand);
			base.AddInclude(x => x.ProductType);
		}

		public ProductsWithRelatedSpecification(ProductSpecificationParams productParams)
			: base(x =>
				(string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
				(!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) && 
				(!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId))
		{
			base.AddInclude(x => x.ProductBrand);
			base.AddInclude(x => x.ProductType);
			base.ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

			if (!string.IsNullOrEmpty(productParams.Sort))
			{
				switch (productParams.Sort)
				{
					case "priceAsc":
						base.AddOrderBy(x => x.Price);
						break;
					case "priceDesc":
						base.AddOrderByDesc(x => x.Price);
						break;
					default:
						base.AddOrderBy(x => x.Name);
						break;
				}
			}
		}
	}
}
