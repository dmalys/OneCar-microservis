using BrandService.DataAccessLayer.Interfaces;
using BrandService.DataAccessLayer.Repository;
using BrandService.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using BrandService.BusinessLayer.Brand.Implementation;
using BrandService.AsyncDataService.EventProcessing;
using BrandService.AsyncDataService;
using System.IO;
using Microsoft.AspNetCore.Http;
using CarModelService.SyncDataServices.gRPC;
using BrandService.SyncDataService.gRPC;

namespace BrandService
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


            services.AddHostedService<MessageBusSubscriber>();

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

            services.AddTransient<IBrandHandler, BrandHandler>();
            services.AddTransient<IBrandRepository, BrandRepository>();//TODO: maybe addscoped?

            services.AddGrpc();
            services.AddScoped<IGrpcCarModelDataClient, GrpcCarModelDataClient>();
            services.AddSingleton<IEventProcessor, EventProcessor>();
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
                endpoints.MapGrpcService<GrpcBrandService>();

                endpoints.MapGet("/proto/brand.proto", async context =>
                {
                    await context.Response.WriteAsync(File.ReadAllText("Proto/brand.proto"));
                });
            });
        }
    }
}
