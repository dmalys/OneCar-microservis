using Newtonsoft.Json;

namespace CarImageService.BusinessLayer.CarImage.Models
{
    public class GetCarImageResponse
    {
        [JsonProperty("carImage")]
        public CarImageDTO CarImage { get; set; }
    }
}
