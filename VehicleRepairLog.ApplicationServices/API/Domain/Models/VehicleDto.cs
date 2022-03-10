using System.Collections.Generic;

namespace VehicleRepairLog.ApplicationServices.API.Domain.Models
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
    }
}
