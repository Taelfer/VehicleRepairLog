using System.ComponentModel.DataAnnotations;

namespace VehicleRepairLog.Shared.DtoModels
{
    public class PartDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Part name is required.")]
        [MaxLength(ErrorMessage = "Maximum length of Part name is 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Part brand name is required.")]
        [MaxLength(ErrorMessage = "Maximum length of Part brand name is 50 characters.")]
        public string BrandName { get; set; }

        [Range(0, 999999999, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public decimal Price { get; set; }

        [Range(0, 50, ErrorMessage = "Amount range should be between 0 and 50.")]
        public short? Amount { get; set; }
        public decimal GetTotalPrice() => (Price * (decimal)Amount);
        public int RepairId { get; set; }
    }
}
