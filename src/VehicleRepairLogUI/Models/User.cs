﻿using System.ComponentModel.DataAnnotations;

namespace VehicleRepairLogUI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string role { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
