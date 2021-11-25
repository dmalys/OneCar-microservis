using AccountService.BusinessLayer.Login.Implementation;
using AccountService.BusinessLayer.Login.Interfaces;
using AccountService.DataAccessLayer.Interfaces;
using AccountService.DataAccessLayer.Repositories;
using AccountService.SyncDataService.gRPC;
using AccountService.SyncDataServices.gRPC;
using AccountService.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;

namespace AccountService
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

            services.AddTransient<IAccountHandler, AccountHandler>();
            services.AddTransient<IAccountRepository, AccountRepository>();

            services.AddGrpc();
            services.AddScoped<IGrpcUserDataClient, GrpcUserDataClient>();
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
                endpoints.MapGrpcService<GrpcAccountService>();

                endpoints.MapGet("/proto/account.proto", async context =>
                {
                    await context.Response.WriteAsync(File.ReadAllText("Proto/account.proto"));
                });
            });
        }
    }
}
