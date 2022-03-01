using MediatR;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Vehicles;

namespace VehicleRepairLog.ApplicationServices.API.Domain.Requests.Vehicles
{
    public class GetVehicleByIdRequest : IRequest<GetVehicleByIdResponse>
    {
        public int VehicleId;
    }
}
