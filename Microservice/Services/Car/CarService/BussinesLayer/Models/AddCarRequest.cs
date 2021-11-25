using System;

namespace CarService.BusinessLayer.Car.Models
{
    public class AddCarRequest : CarDetailedRequest
    {
        public string CreatedBy { get; set; }
    }
}
