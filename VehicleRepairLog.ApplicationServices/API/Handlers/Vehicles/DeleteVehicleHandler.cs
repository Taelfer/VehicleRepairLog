﻿using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain;
using VehicleRepairLog.ApplicationServices.API.Domain.Models;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Vehicles;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Vehicles;
using VehicleRepairLog.ApplicationServices.API.ErrorHandling;
using VehicleRepairLog.DataAccess;
using VehicleRepairLog.DataAccess.CQRS.Commands.Vehicles;
using VehicleRepairLog.DataAccess.CQRS.Queries.Vehicles;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Vehicles
{
    public class DeleteVehicleHandler : IRequestHandler<DeleteVehicleRequest, DeleteVehicleResponse>
    {
        private readonly IMapper mapper;
        private readonly IQueryExecutor queryExecutor;
        private readonly ICommandExecutor commandExecutor;

        public DeleteVehicleHandler(IMapper mapper, IQueryExecutor queryExecutor, ICommandExecutor commandExecutor)
        {
            this.mapper = mapper;
            this.queryExecutor = queryExecutor;
            this.commandExecutor = commandExecutor;
        }

        public async Task<DeleteVehicleResponse> Handle(DeleteVehicleRequest request, CancellationToken cancellationToken)
        {
            var query = new GetVehicleByIdQuery()
            {
                Id = request.VehicleId
            };
            var vehicle = await this.queryExecutor.Execute(query);

            if (vehicle is null)
            {
                return new DeleteVehicleResponse()
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            var command = new DeleteVehicleCommand()
            {
                Parameter = vehicle
            };
            var deletedVehicle = await this.commandExecutor.Execute(command);

            return new DeleteVehicleResponse()
            {
                Data = this.mapper.Map<VehicleDto>(deletedVehicle)
            };
        }
    }
}
