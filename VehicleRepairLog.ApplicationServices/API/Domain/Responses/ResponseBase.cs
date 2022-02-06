using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRepairLog.ApplicationServices.API.Domain.Responses
{
    public class ResponseBase<T>
    {
        public T Data { get; set; }
    }
}
