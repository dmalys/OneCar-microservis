using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.BusinessLayer.Car.Models
{
    public abstract class CarDetailedRequest
    {
        public int? CarImageId { get; set; }

        public int CarModelId { get; set; }

        public DateTime ProductionDate { get; set; }
        
        public int Mileage { get; set; }

        public float PricePerHour { get; set; }

        public string Localization { get; set; }
    }
}
