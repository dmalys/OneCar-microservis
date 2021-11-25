using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarImageService.BusinessLayer.CarImage.Models
{
    public abstract class CarImageIdRequest
    {
        public int CarImageId { get; set; }
    }
}
