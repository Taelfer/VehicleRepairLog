using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Domain.Exceptions;
using VehicleRepairLog.Infrastructure;

namespace VehicleRepairLog.Application.Features.Repairs
{
    public class GetRepairByIdQuery : IRequest<RepairDto>
    {
        public int RepairId;
    }

    public class GetRepairByIdQueryHandler : IRequestHandler<GetRepairByIdQuery, RepairDto>
    {
        private readonly IMapper mapper;
        private readonly VehicleProfileStorageContext context;

        public GetRepairByIdQueryHandler(IMapper mapper, VehicleProfileStorageContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<RepairDto> Handle(GetRepairByIdQuery request, CancellationToken cancellationToken)
        {
            var repair = await this.context.Repairs
                .Include(x => x.Parts)
                .FirstOrDefaultAsync(x => x.Id == request.RepairId);

            if (repair is null)
            {
                throw new NotFoundException("Repair not found.");
            }

            return this.mapper.Map<RepairDto>(repair);
        }
    }
}
