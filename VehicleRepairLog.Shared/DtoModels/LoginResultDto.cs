namespace VehicleRepairLog.Shared.DtoModels
{
    public class LoginResultDto
    {
        public string? Token { get; set; }
        public bool Successful { get; set; } = false;
        public bool IsAuthenticated { get; set; }
    }
}
