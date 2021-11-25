using System;

namespace CarService.BusinessLayer.Car.Models
{
    public class UpdateCarRequest : CarDetailedRequest
    {
        public string UpdatedBy { get; set; }
        public int CarId { get; set; }
    }
}
