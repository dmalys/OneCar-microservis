using LinqToDB.Mapping;
using System;

namespace CarImageService.DataAccessLayer.Entities
{
    [Table(Schema = "dbo", Name = "car_images")]
    public class CarImageEntity : IBaseEntity
    {
        [PrimaryKey, Identity, Column(Name = "car_image_id")]
        public int CarImageId { get; set; }

        [Column(Name = "filename")]
        public string FileName { get; set; }

        [Column(Name = "content", CanBeNull = true)]
        public byte[] Content { get; set; }

        [Column(Name = "create_date")]
        public DateTime CreateDate { get; set; }

        [Column(Name = "update_date")]
        public DateTime? UpdateDate { get; set; }

        [Column(Name = "created_by")]
        public string CreatedBy { get; set; }

        [Column(Name = "updated_by")]
        public string UpdatedBy { get; set; }
    }
}
