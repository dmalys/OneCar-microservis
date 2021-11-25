using Newtonsoft.Json;
using System.Collections.Generic;

namespace CarService.BusinessLayer.Car.Models
{
    public class GetCarsResponse
    {
        [JsonProperty("carList")]
        public List<CarDTO> CarList { get; set; }
    }
}
