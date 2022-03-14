using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain;
using VehicleRepairLog.ApplicationServices.API.Domain.Models;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Vehicles;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Vehicles;
using VehicleRepairLog.ApplicationServices.API.ErrorHandling;
using VehicleRepairLog.DataAccess;
using VehicleRepairLog.DataAccess.CQRS.Queries.Vehicles;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Vehicles
{
    public class GetVehicleByIdHandler : IRequestHandler<GetVehicleByIdRequest, GetVehicleByIdResponse>
    {
        private readonly IMapper mapper;
        private readonly IQueryExecutor queryExecutor;

        public GetVehicleByIdHandler(IMapper mapper, IQueryExecutor queryExecutor)
        {
            this.mapper = mapper;
            this.queryExecutor = queryExecutor;
        }

        public async Task<GetVehicleByIdResponse> Handle(GetVehicleByIdRequest request, CancellationToken cancellationToken)
        {
            var query = new GetVehicleByIdQuery()
            {
                Id = request.VehicleId
            };
            var vehicle = await this.queryExecutor.Execute(query);

            if (vehicle is null)
            {
                return new GetVehicleByIdResponse()
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            return new GetVehicleByIdResponse()
            {
                Data = this.mapper.Map<VehicleDto>(vehicle)
            };
        }
    }
}
