using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VehicleRepairLog.Infrastructure.Entities
{
    public class Vehicle 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BrandName { get; set; }
        public string VinNumber { get; set; }
        public string PaintColor { get; set; }
        public string FuelType { get; set; }

        [Range(0,10000000)]
        public int Mileage { get; set; }

        // Relations.
        public User User { get; set; }
        public int UserId { get; set; }
        public ICollection<Repair> Repairs { get; set; } = new List<Repair>();
    }
}
