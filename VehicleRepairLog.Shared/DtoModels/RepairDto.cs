using System.ComponentModel.DataAnnotations;

namespace VehicleRepairLog.Shared.DtoModels
{
    public class RepairDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [MaxLength(ErrorMessage = "Maximum length of Repair name is 50 characters.")]
        public string Name { get; set; }

        [MaxLength(ErrorMessage = "Maximum length of Description is 1000 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Car workshop name is required.")]
        [MaxLength(ErrorMessage = "Maximum length of Car workshop is 50 characters.")]
        public string CarWorkshopName { get; set; }
        public int VehicleId { get; set; }
    }
}
