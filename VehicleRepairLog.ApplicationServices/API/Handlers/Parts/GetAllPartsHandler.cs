using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Parts;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Parts;
using VehicleRepairLog.ApplicationServices.API.ErrorHandling;
using VehicleRepairLog.DataAccess;
using VehicleRepairLog.DataAccess.CQRS.Queries.Parts;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Parts
{
    public class GetAllPartsHandler : IRequestHandler<GetAllPartsRequest, GetAllPartsResponse>
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IMapper mapper;

        public GetAllPartsHandler(IQueryExecutor queryExecutor, IMapper mapper)
        {
            this.queryExecutor = queryExecutor;
            this.mapper = mapper;
        }

        public async Task<GetAllPartsResponse> Handle(GetAllPartsRequest request, CancellationToken cancellationToken)
        {
            var query = new GetAllPartsQuery()
            {
                Name = request.Name
            };
            var parts = await this.queryExecutor.Execute(query);

            if (parts is null)
            {
                return new GetAllPartsResponse()
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            return new GetAllPartsResponse()
            {
                Data = this.mapper.Map<List<Domain.Models.Part>>(parts)
            };
        }
    }
}
