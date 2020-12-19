using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using ECommerce.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IGenericRepository<Product> _productRepository;
		private readonly IMapper _mapper;

		public ProductsController(IGenericRepository<Product> productRepository, IMapper mapper)
		{
			_productRepository = productRepository;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<ProductDTO>>> GetProducts()
		{
			var spec = new ProductsWithRelatedSpecification();
			var products = await _productRepository.ListAsync(spec);
			return Ok(_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductDTO>>(products));
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ProductDTO>> GetProduct(int id)
		{
			var spec = new ProductsWithRelatedSpecification(id);
			var product = await _productRepository.GetEntityWithSpec(spec);
			
			return Ok(_mapper.Map<Product, ProductDTO>(product));
		}
	}
}
