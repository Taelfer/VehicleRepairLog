using System.Collections.Generic;
using System;

namespace VehicleRepairLog.Application.Models
{
    public class UserViewDto
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
