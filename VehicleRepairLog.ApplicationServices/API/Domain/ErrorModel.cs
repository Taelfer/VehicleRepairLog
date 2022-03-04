namespace VehicleRepairLog.ApplicationServices.API.Domain
{
    /// <summary>
    /// Stores Error data.
    /// </summary>
    public class ErrorModel
    {
        public string Error { get; }

        public ErrorModel(string error)
        {
            this.Error = error;
        }
    }
}
