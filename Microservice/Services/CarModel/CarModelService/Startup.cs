using CarModelService.AsyncDataService;
using CarModelService.BusinessLayer.CarImage.Implementation;
using CarModelService.BusinessLayer.CarImage.Interfaces;
using CarModelService.DataAccessLayer.Interfaces;
using CarModelService.DataAccessLayer.Repository;
using CarModelService.SyncDataService.gRPC;
using CarModelService.SyncDataServices;
using CarModelService.SyncDataServices.gRPC;
using CarModelService.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;

namespace CarModelService
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

            services.AddTransient<ICarModelHandler, CarModelHandler>();
            services.AddTransient<ICarModelsRepository, CarModelsRepository>();// TODO: maybe addscoped?
            services.AddGrpc();
            services.AddScoped<IGrpcBrandDataClient, GrpcBrandDataClient>();
            services.AddScoped<IGrpcCarDataClient, GrpcCarDataClient>();

            services.AddSingleton<IMessageBusClient, MessageBusClient>();

            services.AddHttpClient<IHttpBrandDataClient, HttpBrandDataClient>();
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
                endpoints.MapGrpcService<GrpcCarModelService>();

                endpoints.MapGet("/proto/carmodel.proto", async context =>
                {
                    await context.Response.WriteAsync(File.ReadAllText("Proto/carmodel.proto"));
                });
            });
        }
    }
}
