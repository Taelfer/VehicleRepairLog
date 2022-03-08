using System.ComponentModel.DataAnnotations;

namespace VehicleRepairLog.DataAccess.Entities
{
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
