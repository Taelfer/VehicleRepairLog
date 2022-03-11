using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VehicleRepairLog.DataAccess.Entities
{
    public class User : EntityBase 
    {
        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        //MAKE EMAIL REQUIRED AND USE IT AS LOGIN??????
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }

        [MaxLength(20)]
        public string Role { get; set; }

        [Required]
        [MaxLength (100)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
        public List<Vehicle> Vehicles { get; set; }
    }
}
