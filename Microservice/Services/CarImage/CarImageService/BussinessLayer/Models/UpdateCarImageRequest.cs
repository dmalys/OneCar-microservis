using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CarImageService.BusinessLayer.CarImage.Models
{
    public class UpdateCarImageRequest : CarImageDetailedRequest
    {
        public string UpdatedBy { get; set; }
        public int CarImageId { get; set; }
    }
}
