using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
	[Route("api/types")]
	[ApiController]
	public class ProductTypesController : ControllerBase
	{
		private readonly IGenericRepository<ProductType> _productTypeRepository;

		public ProductTypesController(IGenericRepository<ProductType> productTypeRepository)
		{
			_productTypeRepository = productTypeRepository;
		}

		public async Task<ActionResult<List<ProductType>>> GetProductTypes()
		{
			var types = await _productTypeRepository.ListAllAsync();
			return Ok(types);
		}
	}
}
