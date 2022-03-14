using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain.Models;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Vehicles;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Vehicles;
using VehicleRepairLog.DataAccess;
using VehicleRepairLog.DataAccess.CQRS.Commands.Vehicles;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Vehicles
{
    public class AddVehicleHandler : IRequestHandler<AddVehicleRequest, AddVehicleResponse>
    {
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;

        public AddVehicleHandler(IMapper mapper, ICommandExecutor commandExecutor)
        {
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
        }

        public async Task<AddVehicleResponse> Handle(AddVehicleRequest request, CancellationToken cancellationToken)
        {
            var vehicle = this.mapper.Map<Vehicle>(request);

            var command = new AddVehicleCommand()
            {
                Parameter = vehicle
            };
            var addedVehicle = await this.commandExecutor.Execute(command);

            return new AddVehicleResponse()
            {
                Data = this.mapper.Map<VehicleDto>(addedVehicle)
            };
        }
    }
}
