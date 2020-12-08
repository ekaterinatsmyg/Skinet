﻿using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly StoreContext _storeContext;
		public ProductsController(StoreContext dbContext)
		{
			_storeContext = dbContext;
		}

		[HttpGet]
		public async Task<ActionResult<List<Product>>> GetProducts()
		{
			var products = await _storeContext.Products.ToListAsync();
			return Ok(products);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var product = await _storeContext.Products.FindAsync(id);
			return Ok(product);
		}
	}
}
