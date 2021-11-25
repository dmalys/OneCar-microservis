using LinqToDB.Mapping;
using System;

namespace UserService.DataAccessLayer.Entities
{
    [Table(Schema = "dbo", Name = "users")]
    public class UserEntity : IBaseEntity
    {
        [PrimaryKey, Identity, Column(Name = "user_id")]
        public int UserId { get; set; }

        [Column(Name = "account_id")]
        public int AccountId { get; set; }

        [Column(Name = "car_id", CanBeNull = true)]
        public int? CarId { get; set; }

        [Column(Name = "first_name")]
        public string FirstName { get; set; }

        [Column(Name = "last_name")]
        public string LastName { get; set; }

        [Column(Name = "gender")]
        public string Gender { get; set; }

        [Column(Name = "phone", CanBeNull = true)]
        public string Phone { get; set; }

        [Column(Name = "email")]
        public string Email { get; set; }

        [Column(Name = "street", CanBeNull = true)]
        public string Street { get; set; }

        [Column(Name = "city", CanBeNull = true)]
        public string City { get; set; }

        [Column(Name = "state", CanBeNull = true)]
        public string State { get; set; }

        [Column(Name = "country", CanBeNull = true)]
        public string Country { get; set; }

        [Column(Name = "zip_code", CanBeNull = true)]
        public string ZipCode { get; set; }

        [Column(Name = "available_credit")]
        public float AvailableCredit { get; set; }

        [Column(Name = "driving_license_id")]
        public string DrivingLicenseId { get; set; }

        [Column(Name = "create_date")]
        public DateTime CreateDate { get; set; }

        [Column(Name = "update_date")]
        public DateTime? UpdateDate { get; set; }
    }
}
