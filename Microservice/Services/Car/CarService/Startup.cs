using CarImageService.SyncDataService.gRPC;
using CarModelService.SyncDataService.gRPC;
using CarModelService.SyncDataServices.gRPC;
using CarService.BusinessLayer.Car.Implementation;
using CarService.BusinessLayer.Car.Interfaces;
using CarService.DataAccessLayer.Interfaces;
using CarService.DataAccessLayer.Repository;
using CarService.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;

namespace CarService
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

            services.AddTransient<ICarHandler, CarHandler>();
            services.AddTransient<ICarRepository, CarRepository>();

            services.AddGrpc();
            services.AddScoped<IGrpcCarModelDataClient, GrpcCarModelDataClient>();
            services.AddScoped<IGrpcCarImageDataClient, GrpcCarImageDataClient>();
            services.AddScoped<IGrpcTicketDataClient, GrpcTicketDataClient>();
            //TODO: services.AddSingleton<IMessageBusClient, MessageBusClient>();

            //TODO: services.AddHttpClient<IHttpBrandDataClient, HttpBrandDataClient>();
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
                endpoints.MapGrpcService<GrpcCarService>();

                endpoints.MapGet("/proto/car.proto", async context =>
                {
                    await context.Response.WriteAsync(File.ReadAllText("Proto/car.proto"));
                });
            });
        }
    }
}
