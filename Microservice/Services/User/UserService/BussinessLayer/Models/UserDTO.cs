using System;

namespace UserService.BusinessLayer.User.Models
{
    public class UserDTO
    {

        public int UserId { get; set; }

        public int AccountId { get; set; }

        public int? CarId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string ZipCode { get; set; }

        public float AvailableCredit { get; set; }

        public string DrivingLicenseId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        //public string CreatedBy { get; set; } // moze tu nie byc potrzebne

        //public string UpdatedBy { get; set; } // moze tu nie byc potrzebne

    }
}
