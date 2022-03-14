using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain;
using VehicleRepairLog.ApplicationServices.API.Domain.Models;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Repairs;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Repairs;
using VehicleRepairLog.ApplicationServices.API.ErrorHandling;
using VehicleRepairLog.DataAccess;
using VehicleRepairLog.DataAccess.CQRS.Commands.Repairs;
using VehicleRepairLog.DataAccess.CQRS.Queries.Repairs;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Repairs
{
    public class DeleteRepairHandler : IRequestHandler<DeleteRepairRequest, DeleteRepairResponse>
    {
        private readonly IMapper mapper;
        private readonly IQueryExecutor queryExecutor;
        private readonly ICommandExecutor commandExecutor;

        public DeleteRepairHandler(IMapper mapper, IQueryExecutor queryExecutor, ICommandExecutor commandExecutor)
        {
            this.mapper = mapper;
            this.queryExecutor = queryExecutor;
            this.commandExecutor = commandExecutor;
        }

        public async Task<DeleteRepairResponse> Handle(DeleteRepairRequest request, CancellationToken cancellationToken)
        {
            var query = new GetRepairByIdQuery()
            {
                Id = request.RepairId
            };
            var repair = await this.queryExecutor.Execute(query);

            if (repair is null)
            {
                return new DeleteRepairResponse()
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            var command = new DeleteRepairCommand()
            {
                Parameter = repair
            };
            var deletedRepair = await this.commandExecutor.Execute(command);

            return new DeleteRepairResponse()
            {
                Data = this.mapper.Map<RepairDto>(deletedRepair)
            };
        }
    }
}
