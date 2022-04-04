using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Infrastructure;

namespace VehicleRepairLog.Application.Features.Vehicles
{
    public class GetVehicleByIdQuery : IRequest<VehicleDto>
    {
        public int VehicleId;
    }

    public class GetVehicleByIdQueryHandler : IRequestHandler<GetVehicleByIdQuery, VehicleDto>
    {
        private readonly IMapper mapper;
        private readonly VehicleProfileStorageContext context;

        public GetVehicleByIdQueryHandler(IMapper mapper, VehicleProfileStorageContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<VehicleDto> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
        {
            var vehicle = await this.context.Vehicles
                            .Include(x => x.Repairs)
                            .FirstOrDefaultAsync(x => x.Id == request.VehicleId);

            if (vehicle is null)
            {
                throw new NotFoundException("Vehicle not found.");
            }

            return this.mapper.Map<VehicleDto>(vehicle);
        }
    }
}
