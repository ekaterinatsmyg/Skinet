﻿using AutoMapper;
using Core.Entities;
using ECommerce.DTOs;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Hepers
{
	public class ProductUrlResolver : IValueResolver<Product, ProductDTO, string>
	{
		private readonly IConfiguration _config;

		public ProductUrlResolver(IConfiguration config)
		{
			_config = config;
		}
		public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.PictureUrl))
			{
				return $"{_config["ApiUrl"]}{source.PictureUrl}";
			}

			return null;
		}
	}
}
