using Amazon.Scrapper.EF;
using Amazon.Scrapper.EF.Repositories;
using Amazon.Scrapper.Entities;
using Amazon.Scrapper.Reviews;
using Amazon.Scrapper.ReviewTracking;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Amazon.Scrapper
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Amazon.Scrapper", Version = "v1" });
			});

			services.AddDbContext<AmazonScrapperContext>( options =>
				options.UseSqlite(@"Data Source=C:\AmazonScrapper.db"));

			services.AddTransient<IAmazonScrapper, AmazonScrapper>();
			services.AddTransient<IReviewTracker, ReviewTracker>();
			services.AddTransient<IEmailSender, EmailSender>();
			services.AddTransient<IReviewManager, ReviewManager>();
			services.AddScoped<IRepository<Review>, ReviewRepository>();
			services.AddScoped<IRepository<Product>, ProductRepository>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Amazon.Scrapper v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			using (var scope = app.ApplicationServices.CreateScope())
			{
				scope.ServiceProvider.GetRequiredService<AmazonScrapperContext>().Database.SetCommandTimeout(int.MaxValue);
				scope.ServiceProvider.GetRequiredService<AmazonScrapperContext>().Database.Migrate();
			}
		}
	}
}
