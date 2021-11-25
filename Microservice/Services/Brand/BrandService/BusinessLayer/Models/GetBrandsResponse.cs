using Newtonsoft.Json;
using System.Collections.Generic;

namespace BrandService.BusinessLayer.Brand.Models
{
    public class GetBrandsResponse
    {
        [JsonProperty("brandList")]
        public IList<BrandDTO> BrandList { get; set; }
    }
}
