using System;

namespace CarModelService.BusinessLayer.CarModel.Models
{
    public class CarModelDTO
    {
        public int CarModelId { get; set; }

        public string CarModelName { get; set; }

        public int BrandId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

    }
}
