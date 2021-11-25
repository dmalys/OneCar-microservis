using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarModelService.BusinessLayer.CarModel.Models
{
    public abstract class CarModelIdRequest
    {
        [JsonProperty("carModelId")]
        public int CarModelId { get; set; }
    }
}
