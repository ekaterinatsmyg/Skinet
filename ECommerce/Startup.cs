using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using ECommerce.Hepers;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ECommerce
{
	public class Startup
	{
		private readonly IConfiguration _configuration;
		public Startup(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			services.AddDbContext<StoreContext>(context => context.UseSqlite(_configuration.GetConnectionString("DefaultConnection")));

			services.AddAutoMapper(typeof(MappingProfiles));
			services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
			services.AddScoped<IGenericRepository<ProductBrand>, GenericRepository<ProductBrand>>();
			services.AddScoped<IGenericRepository<ProductType>, GenericRepository<ProductType>>();
			
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseStaticFiles();

			app.UseAuthentication();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGet("/", async context =>
				{
					await context.Response.WriteAsync("Hello World!");
				});

				endpoints.MapControllerRoute(
					name: "getEntities",
					pattern: "api/{controller}/{action}/{id?}");
			});
				
		}
	}
}
