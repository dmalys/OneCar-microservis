using LinqToDB.Mapping;
using System;

namespace BrandService.DataAccessLayer.Entities
{
    [Table(Schema = "dbo", Name = "brands")]
    public class BrandEntity : IBaseEntity
    {
        [PrimaryKey, Identity, Column(Name = "brand_id")]
        public int BrandId { get; set; }

        [Column(Name = "brand_name")]
        public string BrandName { get; set; }

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
