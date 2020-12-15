using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
	[Route("api/brands")]
	[ApiController]
	public class ProductBrandsController : ControllerBase
	{
		private readonly IProductBrandRepository _productBrandRepository;

		public ProductBrandsController(IProductBrandRepository productBrandRepository)
		{
			_productBrandRepository = productBrandRepository;
		}

		[HttpGet]
		public async Task<ActionResult<List<ProductBrand>>> GetBrands()
		{
			var brands = await _productBrandRepository.GetProductBrandsAsync();
			return Ok(brands);
		}
	}
}
