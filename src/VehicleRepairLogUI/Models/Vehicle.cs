using System.ComponentModel.DataAnnotations;

namespace VehicleRepairLogUI.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string BrandName { get; set; }

        [Required(ErrorMessage = "Vin number is required.")]
        public string VinNumber { get; set; }
        public string PaintColor { get; set; }
        public string FuelType { get; set; }
        public int Mileage { get; set; }
        public int UserId { get; set; }
    }
}
