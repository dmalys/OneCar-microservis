using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarImageService.BusinessLayer.CarImage.Models
{
    public abstract class CarImageDetailedRequest
    {
        public string FileName { get; set; }

        public IFormFile Content { get; set; }
    }
}
