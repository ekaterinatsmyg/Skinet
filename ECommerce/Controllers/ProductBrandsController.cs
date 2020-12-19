using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
	[Route("api/brands")]
	[ApiController]
	public class ProductBrandsController : ControllerBase
	{
		private readonly IGenericRepository<ProductBrand> _productBrandRepository;

		public ProductBrandsController(IGenericRepository<ProductBrand> productBrandRepository)
		{
			_productBrandRepository = productBrandRepository;
		}

		[HttpGet]
		public async Task<ActionResult<List<ProductBrand>>> GetBrands()
		{
			var brands = await _productBrandRepository.ListAllAsync();
			return Ok(brands);
		}
	}
}
