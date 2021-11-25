using LinqToDB.Mapping;
using System;

namespace CarService.DataAccessLayer.Entities
{
    [Table(Schema = "dbo", Name = "cars")]
    public class CarEntity : IBaseEntity
    {
        [PrimaryKey, Identity, Column(Name = "car_id")]
        public int CarId { get; set; }

        [Column(Name = "car_image_id", CanBeNull = true)]
        public int? CarImageId { get; set; }

        [Column(Name = "car_model_id")]
        public int CarModelId { get; set; }

        [Column(Name = "production_date")]
        public DateTime ProductionDate { get; set; }

        [Column(Name = "create_date")]
        public DateTime CreateDate { get; set; }

        [Column(Name = "update_date", CanBeNull = true)]
        public DateTime? UpdateDate { get; set; }

        [Column(Name = "created_by")]
        public string CreatedBy { get; set; }

        [Column(Name = "updated_by", CanBeNull = true)]
        public string UpdatedBy { get; set; }

        [Column(Name = "user_rating", CanBeNull = true)]
        public int? UserRating { get; set; } //average

        [Column(Name = "mileage")]
        public int Mileage { get; set; }

        [Column(Name = "price_per_hour")]
        public float PricePerHour { get; set; }

        [Column(Name = "localization")]
        public string Localization { get; set; }
    }
}
