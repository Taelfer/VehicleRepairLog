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

        [MaxLength(ErrorMessage = "Maximum length of Part price is 50 characters.")]
        public decimal Price { get; set; }

        [MaxLength(ErrorMessage = "Maximum amount of parts is 5.")]
        public short? Amount { get; set; }
    }
}
