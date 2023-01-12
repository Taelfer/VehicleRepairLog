using System;
using System.Collections.Generic;

namespace VehicleRepairLog.Infrastructure.Entities
{
    public class Repair
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CarWorkshopName { get; set; }

        // Relations.
        public Vehicle Vehicle { get; set; }
        public int VehicleId { get; set; }
        public ICollection<Part> Parts { get; set; } = new List<Part>();
    }
}
