using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain;
using VehicleRepairLog.ApplicationServices.API.Domain.Models;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Parts;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Parts;
using VehicleRepairLog.ApplicationServices.API.ErrorHandling;
using VehicleRepairLog.DataAccess;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Parts
{
    public class GetPartByIdHandler : IRequestHandler<GetPartByIdRequest, GetPartByIdResponse>
    {
        private readonly IMapper mapper;
        private readonly VehicleProfileStorageContext context;

        public GetPartByIdHandler(IMapper mapper, VehicleProfileStorageContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<GetPartByIdResponse> Handle(GetPartByIdRequest request, CancellationToken cancellationToken)
        {
            var part = await this.context.Parts.FirstOrDefaultAsync(x => x.Id == request.PartId);

            if (part is null)
            {
                return new GetPartByIdResponse()
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            return new GetPartByIdResponse()
            {
                Data = this.mapper.Map<PartDto>(part)
            };
        }
    }
}
