using System.Collections.Generic;

namespace VehicleRepairLog.Infrastructure.Entities
{
    public class Part
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BrandName { get; set; }
        public decimal Price { get; set; }

        // Relation.
        public ICollection<Repair> Repairs { get; set; } = new List<Repair>();
    }
}
