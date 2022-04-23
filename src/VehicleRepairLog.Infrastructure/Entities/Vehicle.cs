using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VehicleRepairLog.Infrastructure.Entities
{
    public class Vehicle 
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(25)]
        public string BrandName { get; set; }

        [Required]
        [StringLength(30)]
        public string VinNumber { get; set; }

        [MaxLength(20)]
        public string PaintColor { get; set; }

        [MaxLength(20)]
        public string FuelType { get; set; }

        [Range(0,10000000)]
        public int Mileage { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public ICollection<Repair> Repairs { get; set; } = new List<Repair>();
    }
}
