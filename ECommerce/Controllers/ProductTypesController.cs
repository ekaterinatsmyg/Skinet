using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
	[Route("api/types")]
	[ApiController]
	public class ProductTypesController : ControllerBase
	{
		private readonly IProductTypeRepository _productTypeRepository;

		public ProductTypesController(IProductTypeRepository productTypeRepository)
		{
			_productTypeRepository = productTypeRepository;
		}

		public async Task<ActionResult<List<ProductType>>> GetProductTypes()
		{
			var types = await _productTypeRepository.GetProductTypesAsync();
			return Ok(types);
		}
	}
}
