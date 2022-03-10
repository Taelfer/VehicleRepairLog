using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Parts;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Parts;
using VehicleRepairLog.ApplicationServices.API.ErrorHandling;
using VehicleRepairLog.DataAccess;
using VehicleRepairLog.DataAccess.CQRS.Commands.Parts;
using VehicleRepairLog.DataAccess.CQRS.Queries.Parts;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Parts
{
    public class DeletePartHandler : IRequestHandler<DeletePartRequest, DeletePartResponse>
    {
        private readonly IMapper mapper;
        private readonly IQueryExecutor queryExecutor;
        private readonly ICommandExecutor commandExecutor;

        public DeletePartHandler(IMapper mapper, IQueryExecutor queryExecutor, ICommandExecutor commandExecutor)
        {
            this.mapper = mapper;
            this.queryExecutor = queryExecutor;
            this.commandExecutor = commandExecutor;
        }

        public async Task<DeletePartResponse> Handle(DeletePartRequest request, CancellationToken cancellationToken)
        {
            var query = new GetPartByIdQuery()
            {
                Id = request.PartId
            };
            var part = await this.queryExecutor.Execute(query);

            if (part is null)
            {
                return new DeletePartResponse()
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            var command = new DeletePartCommand()
            {
                Parameter = part
            };
            var deletedPart = await this.commandExecutor.Execute(command);

            return new DeletePartResponse()
            {
                Data = this.mapper.Map<Domain.Models.PartDto>(deletedPart)
            };
        }
    }
}
