using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.BusinessLayer.User.Models
{
    public abstract class UserDetailedRequest
    {
        public int AccountId { get; set; }

        //public int? CarId { get; set; }

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
    }
}
