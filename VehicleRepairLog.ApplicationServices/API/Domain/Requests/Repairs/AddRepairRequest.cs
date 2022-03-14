using MediatR;
using System;
using System.Collections.Generic;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Repairs;

namespace VehicleRepairLog.ApplicationServices.API.Domain.Requests.Repairs
{
    public class AddRepairRequest : IRequest<AddRepairResponse>
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string CarWorkshopName { get; set; }
        public int VehicleId { get; set; }
        public List<string> PartNames { get; set; }
    }
}
