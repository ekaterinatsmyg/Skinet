using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using ECommerce.DTOs;
using ECommerce.Errors;
using ECommerce.Hepers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
	public class ProductsController : BaseApiController
	{
		private readonly IGenericRepository<Product> _productRepository;
		private readonly IMapper _mapper;

		public ProductsController(IGenericRepository<Product> productRepository, IMapper mapper)
		{
			_productRepository = productRepository;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<Pagination<ProductDTO>>> GetProducts(
			[FromQuery] ProductSpecificationParams productParams)
		{
			var spec = new ProductsWithRelatedSpecification(productParams);

			var countSpec = new ProductsWithFiltersForCountSpecification(productParams);

			var totalItems = await _productRepository.CountAsync(countSpec);

			var products = await _productRepository.ListAsync(spec);

			var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDTO>>(products);

			return Ok(new Pagination<ProductDTO>()
			{
				Count = totalItems,
				Data = data,
				PageIndex = productParams.PageIndex,
				PageSize = productParams.PageSize,
			});
		}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ProductDTO>> GetProduct(int id)
		{
			var spec = new ProductsWithRelatedSpecification(id);
			var product = await _productRepository.GetEntityWithSpec(spec);

			if (product == null)
			{
				return NotFound(new ApiResponse((int)HttpStatusCode.NotFound));
			}

			return Ok(_mapper.Map<Product, ProductDTO>(product));
		}
	}
}
