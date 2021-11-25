using Newtonsoft.Json;
using System.Collections.Generic;

namespace CarModelService.BusinessLayer.CarModel.Models
{
    public class GetCarModelsResponse
    {
        [JsonProperty("carModelList")]
        public IList<CarModelDTO> CarModelList { get; set; }
    }
}
