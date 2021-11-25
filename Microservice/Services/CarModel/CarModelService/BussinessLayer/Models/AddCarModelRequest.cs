namespace CarModelService.BusinessLayer.CarModel.Models
{
    public class AddCarModelRequest : CarModelDetailedRequest
    {
        public string CreatedBy { get; set; }
    }
}
