using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VehicleRepairLog.DataAccess.Entities
{
    public class Repair : EntityBase
    {
        [Required]
        public DateTime Date { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [MaxLength(1000)]
        public string CarWorkshopName { get; set; }
        public Vehicle Vehicle { get; set; }
        public int VehicleId { get; set; }
        public List<Part> Parts { get; set; }
    }
}
