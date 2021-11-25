using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.BusinessLayer.User.Models
{
    public class RentCarRequest
    {
        public int UserId { get; set; }

        public int CarId { get; set; }

        public int AmountOfHours { get; set; }
    }
}
