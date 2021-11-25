using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarModelService.DataAccessLayer.Entities
{
    [Table(Schema = "dbo", Name = "car_models")]
    public class CarModelEntity : IBaseEntity
    {
        [PrimaryKey, Identity, Column(Name = "car_model_id")]
        public int CarModelId { get; set; }

        [Column(Name = "brand_id")]
        public int BrandId { get; set; }

        [Column(Name = "car_model_name")]
        public string CarModelName { get; set; }

        [Column(Name = "create_date")]
        public DateTime CreateDate { get; set; }

        [Column(Name = "update_date", CanBeNull = true)]
        public DateTime? UpdateDate { get; set; }

        [Column(Name = "created_by")]
        public string CreatedBy { get; set; }

        [Column(Name = "updated_by", CanBeNull = true)]
        public string UpdatedBy { get; set; }
    }
}
