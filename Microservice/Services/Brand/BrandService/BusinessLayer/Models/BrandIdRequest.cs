using Newtonsoft.Json;

namespace BrandService.BusinessLayer.Brand.Models
{
    public abstract class BrandIdRequest
    {
        [JsonProperty("brandId")]
        public int BrandId { get; set; }
    }
}
