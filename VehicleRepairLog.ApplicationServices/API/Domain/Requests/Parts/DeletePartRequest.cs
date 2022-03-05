using MediatR;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Parts;

namespace VehicleRepairLog.ApplicationServices.API.Domain.Requests.Parts
{
    public class DeletePartRequest : IRequest<DeletePartResponse>
    {
        public int PartId;
    }
}
