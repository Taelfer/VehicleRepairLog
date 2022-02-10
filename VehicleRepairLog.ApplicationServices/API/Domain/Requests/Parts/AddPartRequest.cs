using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Parts;

namespace VehicleRepairLog.ApplicationServices.API.Domain.Requests.Parts
{
    public class AddPartRequest : IRequest<AddPartResponse>
    {
        public string Name { get; set; }
        public string BrandName { get; set; }
        public decimal Price { get; set; }
    }
}
