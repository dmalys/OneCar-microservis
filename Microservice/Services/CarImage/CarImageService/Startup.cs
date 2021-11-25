using BrandService.SyncDataService.gRPC;
using CarImageService.BusinessLayer.CarImage.Implementation;
using CarImageService.BusinessLayer.CarImage.Interfaces;
using CarImageService.DataAccessLayer.Interfaces;
using CarImageService.DataAccessLayer.Repositories;
using CarImageService.Utility;
using CarModelService.SyncDataServices.gRPC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;

namespace CarImageService
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

            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My Awesome API",
                    Version = "v1"
                });
                //  c.OperationFilter<SwaggerFileOperationFilter>();

            });

            services.AddTransient<ICarImageHandler, CarImageHandler>();
            services.AddTransient<ICarImageRepository, CarImageRepository>();

            services.AddGrpc();
            services.AddScoped<IGrpcCarDataClient, GrpcCarDataClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "My API");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseApiExceptionHandling();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGrpcService<GrpcCarImageService>();

                endpoints.MapGet("/proto/carimage.proto", async context =>
                {
                    await context.Response.WriteAsync(File.ReadAllText("Proto/carimage.proto"));
                });
            });
        }
    }
}
