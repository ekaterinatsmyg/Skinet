using Core.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
	public class StoreContextSeed
	{
		private const string RELATIVE_PATH = "../Infrastructure/Data/SeedData/";
		public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
		{
			try
			{
				if(!context.ProductBrands.Any())
				{
					var brandsData = File.ReadAllText($"{RELATIVE_PATH}brands.json");
					var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

					foreach(var item in brands)
					{
						context.ProductBrands.Add(item);
					}

					await context.SaveChangesAsync();
				}

				if (!context.ProductTypes.Any())
				{
					var productTypesData = File.ReadAllText($"{RELATIVE_PATH}types.json");
					var productTypes = JsonSerializer.Deserialize<List<ProductType>>(productTypesData);

					foreach (var item in productTypes)
					{
						context.ProductTypes.Add(item);
					}

					await context.SaveChangesAsync();
				}

				if (!context.Products.Any())
				{
					
					var productTypesData = File.ReadAllText($"{RELATIVE_PATH}products.json");
					var productTypes = JsonSerializer.Deserialize<List<Product>>(productTypesData);

					foreach (var item in productTypes)
					{
						context.Products.Add(item);
					}

					await context.SaveChangesAsync();
				}
			}
			catch (Exception ex)
			{
				var logger = loggerFactory.CreateLogger<StoreContextSeed>();
				logger.LogError(ex.Message);
			}
		}
	}
}
