using Core.Entities;
using Core.Interfaces;
using ECommerce.Errors;
using Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace ECommerce.Extensions
{
	public static class ApplicationServicesExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
			services.AddScoped<IGenericRepository<ProductBrand>, GenericRepository<ProductBrand>>();
			services.AddScoped<IGenericRepository<ProductType>, GenericRepository<ProductType>>();

			services.Configure<ApiBehaviorOptions>(opt =>
			{
				opt.InvalidModelStateResponseFactory = actionContext =>
				{
					var errors = actionContext.ModelState
					.Where(e => e.Value.Errors.Count > 0)
					.SelectMany(x => x.Value.Errors)
					.Select(x => x.ErrorMessage)
					.ToArray();

					var errorResponse = new ApiValidationErrorResponse()
					{
						Errors = errors
					};

					return new BadRequestObjectResult(errorResponse);
				};
			});

			return services;
		}
	}
}
