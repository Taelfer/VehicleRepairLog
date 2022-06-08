namespace VehicleRepairLogUI.Models
{
    public class LoginResult
    {
        public string? Token { get; set; }
        public bool Successful { get; set; } = false;
        public bool IsAuthenticated { get; set; }
    }
}
