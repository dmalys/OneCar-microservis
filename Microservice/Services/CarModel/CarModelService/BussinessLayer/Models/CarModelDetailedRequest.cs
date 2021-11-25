using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarModelService.BusinessLayer.CarModel.Models
{
    public abstract class CarModelDetailedRequest
    {
        public string CarModelName { get; set; }
        
        public int BrandId { get; set; }
    }
}
