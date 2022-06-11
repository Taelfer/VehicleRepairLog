using System;

namespace VehicleRepairLog.Application.Exceptions
{
    public class NotFoundRepairException : Exception
    {
        public NotFoundRepairException(string message) : base(message) { }
    }
}
