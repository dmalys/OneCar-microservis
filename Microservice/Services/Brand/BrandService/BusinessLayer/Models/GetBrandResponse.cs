using Newtonsoft.Json;

namespace BrandService.BusinessLayer.Brand.Models
{
    public class GetBrandResponse
    {
        [JsonProperty("brand")]
        public BrandDTO Brand { get; set; }
    }
}
