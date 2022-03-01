using MediatR;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Vehicles;

namespace VehicleRepairLog.ApplicationServices.API.Domain.Requests.Vehicles
{
    public class DeleteVehicleRequest : IRequest<DeleteVehicleResponse>
    {
        public int VehicleId;
    }
}
