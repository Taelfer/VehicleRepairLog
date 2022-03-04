using AutoMapper;
using MediatR;
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
    public class GetPartByIdHandler : IRequestHandler<GetPartByIdRequest, GetPartByIdResponse>
    {
        private readonly IMapper mapper;
        private readonly IQueryExecutor queryExecutor;

        public GetPartByIdHandler(IMapper mapper, IQueryExecutor queryExecutor)
        {
            this.mapper = mapper;
            this.queryExecutor = queryExecutor;
        }

        public async Task<GetPartByIdResponse> Handle(GetPartByIdRequest request, CancellationToken cancellationToken)
        {
            var query = new GetPartByIdQuery() 
            {
                Id = request.PartId
            };

            var part = await this.queryExecutor.Execute(query);

            if (part == null)
            {
                return new GetPartByIdResponse()
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            var response = new GetPartByIdResponse()
            {
                Data = this.mapper.Map<Domain.Models.Part>(part)
            };

            return response;
        }
    }
}
