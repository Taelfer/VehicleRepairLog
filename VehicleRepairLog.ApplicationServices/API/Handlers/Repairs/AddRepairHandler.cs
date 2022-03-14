using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain.Models;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Repairs;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Repairs;
using VehicleRepairLog.DataAccess;
using VehicleRepairLog.DataAccess.CQRS.Commands.Repairs;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Repairs
{
    public class AddRepairHandler : IRequestHandler<AddRepairRequest, AddRepairResponse>
    {
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;

        public AddRepairHandler(IMapper mapper, ICommandExecutor commandExecutor)
        {
            this.mapper = mapper;
            this.commandExecutor = commandExecutor;
        }

        public async Task<AddRepairResponse> Handle(AddRepairRequest request, CancellationToken cancellationToken)
        {
            var repair = this.mapper.Map<Repair>(request);

            var command = new AddRepairCommand()
            {
                Parameter = repair
            };
            var addedRepair = await this.commandExecutor.Execute(command);

            return new AddRepairResponse()
            {
                Data = this.mapper.Map<RepairDto>(addedRepair)
            };
        }
    }
}
