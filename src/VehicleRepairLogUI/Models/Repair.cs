namespace VehicleRepairLogUI.Models
{
    public class Repair
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CarWorkshopName { get; set; }
    }
}
