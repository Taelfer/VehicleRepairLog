using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Parts;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Parts;
using VehicleRepairLog.DataAccess;
using VehicleRepairLog.DataAccess.CQRS.Commands;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Parts
{
    public class AddPartHandler : IRequestHandler<AddPartRequest, AddPartResponse>
    {
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        public AddPartHandler(IMapper mapper, ICommandExecutor commandExecutor)
        {
            this.commandExecutor = commandExecutor;
            this.mapper = mapper;
        }

        public async Task<AddPartResponse> Handle(AddPartRequest request, CancellationToken cancellationToken)
        {
            var part = this.mapper.Map<Part>(request);
            var command = new AddPartCommand()
            {
                Parameter = part
            };
            var commandFromDb = await this.commandExecutor.Execute(command);

            return new AddPartResponse()
            {
                Data = this.mapper.Map<Domain.Models.PartDto>(commandFromDb)
            };
        }
    }
}
