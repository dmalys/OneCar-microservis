using System;

namespace BrandService.BusinessLayer.Brand.Models
{
    public class BrandDTO
    {
        public int BrandId { get; set; }

        public string BrandName { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }
    }
}
