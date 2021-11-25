using Newtonsoft.Json;

namespace CarService.BusinessLayer.Car.Models
{
    public class GetCarResponse
    {
        [JsonProperty("car")]
        public CarDTO Car { get; set; }
    }
}
