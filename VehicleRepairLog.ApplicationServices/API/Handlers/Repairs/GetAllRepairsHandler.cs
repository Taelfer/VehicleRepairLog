using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
    public class GetAllRepairsHandler : IRequestHandler<GetAllRepairsRequest, GetAllRepairsResponse>
    {
        private readonly IMapper mapper;
        private readonly VehicleProfileStorageContext context;

        public GetAllRepairsHandler(IMapper mapper, VehicleProfileStorageContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<GetAllRepairsResponse> Handle(GetAllRepairsRequest request, CancellationToken cancellationToken)
        {
            var repairs = await this.context.Repairs
                .Include(x => x.Parts)
                .ToListAsync();

            if (repairs is null)
            {
                return new GetAllRepairsResponse()
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            return new GetAllRepairsResponse()
            {
                Data = this.mapper.Map<List<RepairDto>>(repairs)
            };
        }
    }
}
