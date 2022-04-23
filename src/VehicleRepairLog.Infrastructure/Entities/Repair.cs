using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VehicleRepairLog.Infrastructure.Entities
{
    public class Repair
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [MaxLength(1000)]
        public string CarWorkshopName { get; set; }
        public Vehicle Vehicle { get; set; }
        public int VehicleId { get; set; }
        public ICollection<Part> Parts { get; set; } = new List<Part>();
    }
}
