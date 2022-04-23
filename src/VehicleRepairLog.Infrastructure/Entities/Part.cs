using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VehicleRepairLog.Infrastructure.Entities
{
    public class Part
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string BrandName { get; set; }
        public decimal Price { get; set; }
        public ICollection<Repair> Repairs { get; set; } = new List<Repair>();
    }
}
