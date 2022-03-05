using MediatR;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Parts;

namespace VehicleRepairLog.ApplicationServices.API.Domain.Requests.Parts
{
    public class GetPartByIdRequest : IRequest<GetPartByIdResponse>
    {
        public int PartId { get; set; }
    }
}
