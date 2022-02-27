using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Parts;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Parts;
using VehicleRepairLog.DataAccess;
using VehicleRepairLog.DataAccess.CQRS.Commands.Parts;
using VehicleRepairLog.DataAccess.CQRS.Queries.Parts;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Parts
{
    public class UpdatePartHandler : IRequestHandler<UpdatePartRequest, UpdatePartResponse>
    {
        private readonly IMapper mapper;
        private readonly IQueryExecutor queryExecutor;
        private readonly ICommandExecutor commandExecutor;

        public UpdatePartHandler(IMapper mapper, IQueryExecutor queryExecutor, ICommandExecutor commandExecutor)
        {
            this.mapper = mapper;
            this.queryExecutor = queryExecutor;
            this.commandExecutor = commandExecutor;
        }

        public async Task<UpdatePartResponse> Handle(UpdatePartRequest request, CancellationToken cancellationToken)
        {
            var query = new GetPartByIdQuery()
            {
                Id = request.PartId
            };
            var part = await this.queryExecutor.Execute(query);

            var command = new UpdatePartCommand()
            {
                Parameter = this.mapper.Map(request, part)
            };
            var updatePart = await this.commandExecutor.Execute(command);

            return new UpdatePartResponse()
            {
                Data = this.mapper.Map<Domain.Models.Part>(updatePart)
            };
        }
    }
}
