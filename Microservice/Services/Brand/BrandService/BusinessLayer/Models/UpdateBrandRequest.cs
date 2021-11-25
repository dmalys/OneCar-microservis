namespace BrandService.BusinessLayer.Brand.Models
{
    public class UpdateBrandRequest : BrandDetailedRequest
    {                
        public string UpdatedBy { get; set; }

        public int BrandId { get; set; }
    }
}
