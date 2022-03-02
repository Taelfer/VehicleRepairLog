using System;
using System.Collections.Generic;

namespace VehicleRepairLog.DataAccess.Entities
{
    public class Repair : EntityBase
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string CarWorkshopName { get; set; }
        public Vehicle Vehicle { get; set; }
        public int VehicleId { get; set; }
        public List<Part> Parts { get; set; }
    }
}
