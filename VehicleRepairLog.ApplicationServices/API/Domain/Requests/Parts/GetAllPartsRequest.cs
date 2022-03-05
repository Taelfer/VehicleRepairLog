using MediatR;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Parts;

namespace VehicleRepairLog.ApplicationServices.API.Domain.Requests.Parts
{
    public class GetAllPartsRequest : IRequest<GetAllPartsResponse>
    {
        public string Name { get; set; }
    }
}
