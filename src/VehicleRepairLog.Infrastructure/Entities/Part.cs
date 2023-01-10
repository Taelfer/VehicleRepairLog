namespace VehicleRepairLog.Infrastructure.Entities
{
    public class Part
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BrandName { get; set; }
        public decimal? Price { get; set; }
        public short? Amount { get; set; }

        // Relation.
        public Repair Repair { get; set; }
        public int RepairId { get; set; }
    }
}
