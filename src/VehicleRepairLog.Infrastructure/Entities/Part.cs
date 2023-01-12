using System.ComponentModel.DataAnnotations;

namespace VehicleRepairLog.Infrastructure.Entities
{
    public class Part
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BrandName { get; set; }

        [Range(0, 999999999)]
        public decimal? Price { get; set; }

        [Range(0, 50)]
        public short? Amount { get; set; }

        // Relation.
        public Repair Repair { get; set; }
        public int RepairId { get; set; }
    }
}
