using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Vehicles;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Vehicles;
using VehicleRepairLog.DataAccess;
using VehicleRepairLog.DataAccess.CQRS.Commands.Vehicles;
using VehicleRepairLog.DataAccess.CQRS.Queries.Vehicles;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Vehicles
{
    public class UpdateVehicleHandler : IRequestHandler<UpdateVehicleRequest, UpdateVehicleResponse>
    {
        private readonly IMapper mapper;
        private readonly IQueryExecutor queryExecutor;
        private readonly ICommandExecutor commandExecutor;

        public UpdateVehicleHandler(IMapper mapper, IQueryExecutor queryExecutor, ICommandExecutor commandExecutor)
        {
            this.mapper = mapper;
            this.queryExecutor = queryExecutor;
            this.commandExecutor = commandExecutor;
        }

        public async Task<UpdateVehicleResponse> Handle(UpdateVehicleRequest request, CancellationToken cancellationToken)
        {
            var query = new GetVehicleByIdQuery()
            {
                Id = request.VehicleId
            };
            var vehicle = await this.queryExecutor.Execute(query);

            var command = new UpdateVehicleCommand()
            {
                Parameter = this.mapper.Map(request, vehicle)
            };
            var updatedVehicle = await this.commandExecutor.Execute(command);

            return new UpdateVehicleResponse()
            {
                Data = this.mapper.Map<Domain.Models.VehicleDto>(updatedVehicle)
            };
        }
    }
}
