using Newtonsoft.Json;
using System;

namespace CarService.BusinessLayer.Car.Models
{
    public class CarDTO
    {
        [JsonProperty("carId")]
        public int CarId { get; set; }

        [JsonProperty("carImageId")]
        public int? CarImageId { get; set; }

        [JsonProperty("carModelId")]
        public int CarModelId { get; set; }

        [JsonProperty("productionDate")]
        public DateTime ProductionDate { get; set; }

        [JsonProperty("createDate")]
        public DateTime CreateDate { get; set; }

        [JsonProperty("updateDate")]
        public DateTime? UpdateDate { get; set; }

        [JsonProperty("createdBy")]
        public string CreatedBy { get; set; }

        [JsonProperty("updatedBy")]
        public string UpdatedBy { get; set; }

        [JsonProperty("userRating")]
        public int? UserRating { get; set; } //average

        [JsonProperty("mileage")]
        public int Mileage { get; set; }

        [JsonProperty("pricePerHour")]
        public float PricePerHour { get; set; }

        [JsonProperty("localization")]
        public string Localization { get; set; }
    }
}
