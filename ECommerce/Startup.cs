using AutoMapper;
using ECommerce.Extensions;
using ECommerce.Hepers;
using ECommerce.Middleware;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

			services.AddApplicationServices();

			services.AddSwaggerDocumentation();

			services.AddCors(opt =>
			{
				opt.AddPolicy("CorsPolicy", policy =>
				{

					policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
				});
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseMiddleware<ExceptionMiddleware>();

			app.UseStatusCodePagesWithReExecute("errors/{0}");

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseStaticFiles();

			app.UseAuthentication();

			app.UseCors("CorsPolicy");

			app.UseAuthorization();

			app.UseSwaggerDocumentation();

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
