using System;

namespace VehicleRepairLog.Application.Exceptions
{
    public class NotAuthenticatedException : Exception
    {
        public NotAuthenticatedException(string message) : base(message)
        {
        }
    }
}
