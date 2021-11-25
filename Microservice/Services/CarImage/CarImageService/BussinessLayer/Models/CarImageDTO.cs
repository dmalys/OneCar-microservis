using System;

namespace CarImageService.BusinessLayer.CarImage.Models
{
    public class CarImageDTO
    {
        public int CarImageId { get; set; }

        public string FileName { get; set; }

        public byte[] Content { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }
    }
}
