﻿using MediatR;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Parts;

namespace VehicleRepairLog.ApplicationServices.API.Domain.Requests.Parts
{
    public class UpdatePartRequest : IRequest<UpdatePartResponse>
    {
        public int PartId;
        public string Name { get; set; }
        public string BrandName { get; set; }
        public decimal Price { get; set; }
    }
}
