using MediatR;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Vehicles;

namespace VehicleRepairLog.ApplicationServices.API.Domain.Requests.Vehicles
{
    public class GetAllVehiclesRequest : IRequest<GetAllVehiclesResponse>
    {
    }
}
