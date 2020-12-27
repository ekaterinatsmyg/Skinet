using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
	[Route("api/types")]
	public class ProductTypesController : BaseApiController
	{
		private readonly IGenericRepository<ProductType> _productTypeRepository;

		public ProductTypesController(IGenericRepository<ProductType> productTypeRepository)
		{
			_productTypeRepository = productTypeRepository;
		}

		[HttpGet]
		public async Task<ActionResult<List<ProductType>>> GetProductTypes()
		{
			var types = await _productTypeRepository.ListAllAsync();
			return Ok(types);
		}
	}
}
