using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
	public class ProductsWithRelatedSpecification : BaseSpecification<Product>
	{
		public ProductsWithRelatedSpecification()
		{
			base.AddInclude(x => x.ProductBrand);
			base.AddInclude(x => x.ProductType);
		}

		public ProductsWithRelatedSpecification(int id) : base(x => x.Id == id)
		{
			base.AddInclude(x => x.ProductBrand);
			base.AddInclude(x => x.ProductType);
		}
	}
}
