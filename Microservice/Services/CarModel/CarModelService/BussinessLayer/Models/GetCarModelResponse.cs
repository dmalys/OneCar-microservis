using Newtonsoft.Json;

namespace CarModelService.BusinessLayer.CarModel.Models
{
    public class GetCarModelResponse
    {
        [JsonProperty("carModel")]
        public CarModelDTO CarModel { get; set; }
    }
}
