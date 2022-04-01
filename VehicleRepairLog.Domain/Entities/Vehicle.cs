using System.ComponentModel.DataAnnotations;

namespace VehicleRepairLog.Domain.Entities
{
    public class Vehicle 
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string BrandName { get; set; }

        [Required]
        [MaxLength(100)]
        public string VinNumber { get; set; }

        [MaxLength(100)]
        public string PaintColor { get; set; }

        [MaxLength(100)]
        public string FuelType { get; set; }

        [Range(0,10000000)]
        public int Mileage { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public List<Repair> Repairs { get; set; }
    }
}
