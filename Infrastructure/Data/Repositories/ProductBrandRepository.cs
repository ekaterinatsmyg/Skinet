using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
	public class ProductBrandRepository : IProductBrandRepository
	{
		private readonly StoreContext _context;
		public ProductBrandRepository(StoreContext context)
		{
			_context = context;
		}
		public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
		{
			return await _context.ProductBrands.ToListAsync();
		}
	}
}
