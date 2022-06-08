using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VehicleRepairLog.Infrastructure.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public UserRole Role { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        // Relation.
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
