using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Infrastructure;

namespace VehicleRepairLog.Application.Features.Repairs
{
    public class GetAllRepairsQuery : IRequest<List<RepairDto>>
    {
    }

    public class GetAllRepairsQueryHandler : IRequestHandler<GetAllRepairsQuery, List<RepairDto>>
    {
        private readonly IMapper mapper;
        private readonly VehicleProfileStorageContext context;

        public GetAllRepairsQueryHandler(IMapper mapper, VehicleProfileStorageContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<List<RepairDto>> Handle(GetAllRepairsQuery request, CancellationToken cancellationToken)
        {
            var repairs = await this.context.Repairs
                .Include(x => x.Parts)
                .ToListAsync();

            if (repairs is null)
            {
                throw new NotFoundException("Repairs not found.");
            }

            return this.mapper.Map<List<RepairDto>>(repairs);
        }
    }
}
