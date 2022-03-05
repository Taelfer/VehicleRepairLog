﻿using System;
using System.Collections.Generic;

namespace VehicleRepairLog.ApplicationServices.API.Domain.Models
{
    public class Repair
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string CarWorkshopName { get; set; }
        public int VehicleId { get; set; }
        public List<string> PartNames { get; set; }
    }
}
