using System;
using System.Collections.Generic;
using VehicleRepairLog.DataAccess;

namespace VehicleRepairLog.ApplicationServices.API.Domain.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public List<string> VehiclesBrandName { get; set; }
    }
}
