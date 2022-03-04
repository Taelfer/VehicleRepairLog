namespace VehicleRepairLog.ApplicationServices.API.Domain.Responses
{
    public abstract class ResponseBase<T> : ErrorResponseBase
    {
        public T Data { get; set; }
    }
}
