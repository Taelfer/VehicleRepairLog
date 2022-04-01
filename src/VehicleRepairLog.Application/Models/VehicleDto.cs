using System.Collections.Generic;

namespace VehicleRepairLog.Application.Models
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string VinNumber { get; set; }
        public string PaintColor { get; set; }
        public string FuelType { get; set; }
        public int Mileage { get; set; }
        public int UserId { get; set; }
        public List<string> RepairDescriptions { get; set; }
    }
}
