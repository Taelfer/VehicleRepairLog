namespace VehicleRepairLog.Shared.DtoModels
{
    public class RegisterResultDto
    {
        public bool Successful { get; set; }
        public IEnumerable<string> Errors { get; set; } = new List<string>();
    }
}
