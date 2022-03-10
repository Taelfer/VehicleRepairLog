using MediatR;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Users;

namespace VehicleRepairLog.ApplicationServices.API.Domain.Requests.Users
{
    public class GetAllUsersRequest : IRequest<GetAllUsersResponse>
    {
    }
}
