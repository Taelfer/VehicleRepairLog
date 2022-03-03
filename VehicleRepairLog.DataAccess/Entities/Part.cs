using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VehicleRepairLog.DataAccess.Entities
{
    public class Part : EntityBase
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string BrandName { get; set; }
        public decimal Price { get; set; }
        public List<Repair> Repairs { get; set; }
    }
}
