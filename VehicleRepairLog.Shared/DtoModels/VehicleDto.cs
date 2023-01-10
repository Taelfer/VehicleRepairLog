using System.ComponentModel.DataAnnotations;

namespace VehicleRepairLog.Shared.DtoModels
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BrandName { get; set; }

        [Required(ErrorMessage = "VIN number is required.")]
        public string VinNumber { get; set; }
        public string PaintColor { get; set; }
        public string FuelType { get; set; }
        public int Mileage { get; set; }
        public int UserId { get; set; }
        public List<RepairDto> Repairs { get; set; }
    }
}
