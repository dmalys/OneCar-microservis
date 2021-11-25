using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;
using UserService.BusinessLayer.User.Implementation;
using UserService.BusinessLayer.User.Interfaces;
using UserService.DataAccessLayer.Interfaces;
using UserService.DataAccessLayer.Repositories;
using UserService.SyncDataService.gRPC;
using UserService.SyncDataServices.gRPC;
using UserService.Utility;

namespace UserService
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

            services.AddTransient<IUserHandler, UserHandler>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddGrpc();
            services.AddScoped<IGrpcAccountDataClient, GrpcAccountDataClient>();
            services.AddScoped<IGrpcCouponDataClient, GrpcCouponDataClient>();
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
                endpoints.MapGrpcService<GrpcUserService>();

                endpoints.MapGet("/proto/user.proto", async context =>
                {
                    await context.Response.WriteAsync(File.ReadAllText("Proto/user.proto"));
                });
            });
        }
    }
}
