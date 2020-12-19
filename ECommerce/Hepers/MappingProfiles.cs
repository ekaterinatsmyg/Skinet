using AutoMapper;
using Core.Entities;
using ECommerce.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Hepers
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<Product, ProductDTO>()
				.ForMember(d => d.ProductBrand, o => o.MapFrom(src => src.ProductBrand.Name))
				.ForMember(d => d.ProductType, o => o.MapFrom(src => src.ProductType.Name))
				.ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
		}
	}
}
