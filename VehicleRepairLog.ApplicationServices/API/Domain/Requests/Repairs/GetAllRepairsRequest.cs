using MediatR;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Repairs;

namespace VehicleRepairLog.ApplicationServices.API.Domain.Requests.Repairs
{
    public class GetAllRepairsRequest : IRequest<GetAllRepairsResponse>
    {

    }
}
