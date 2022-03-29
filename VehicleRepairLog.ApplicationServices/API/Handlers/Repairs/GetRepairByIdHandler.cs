using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain;
using VehicleRepairLog.ApplicationServices.API.Domain.Models;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Repairs;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Repairs;
using VehicleRepairLog.ApplicationServices.API.ErrorHandling;
using VehicleRepairLog.DataAccess;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Repairs
{
    public class GetRepairByIdHandler : IRequestHandler<GetRepairByIdRequest, GetRepairByIdResponse>
    {
        private readonly IMapper mapper;
        private readonly VehicleProfileStorageContext context;

        public GetRepairByIdHandler(IMapper mapper, VehicleProfileStorageContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<GetRepairByIdResponse> Handle(GetRepairByIdRequest request, CancellationToken cancellationToken)
        {
            var repair = await this.context.Repairs
                .Include(x => x.Parts)
                .FirstOrDefaultAsync(x => x.Id == request.RepairId);

            if (repair is null)
            {
                return new GetRepairByIdResponse()
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            return new GetRepairByIdResponse()
            {
                Data = this.mapper.Map<RepairDto>(repair)
            };
        }
    }
}
