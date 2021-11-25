using CarModelService.SyncDataServices.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarModelService.SyncDataServices
{
    public class HttpBrandDataClient : IHttpBrandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpBrandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task SendCheckForAddCarModel(SendAddCarModelRequest request)
        {
            var httpContent = new StringContent(
                 JsonSerializer.Serialize(request),
                 Encoding.UTF8,
                 "application/json");

            var response = await _httpClient.PostAsync($"{_configuration["BrandServiceBaseUrl"]}", httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to CommandService was OK!");
            }
            else
            {
                Console.WriteLine("--> Sync POST to CommandService was NOT OK!");
            }
        }
    }
}
