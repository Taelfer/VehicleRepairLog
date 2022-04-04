using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Infrastructure;

namespace VehicleRepairLog.Application.Features.Repairs
{
    public class UpdateRepairCommand : IRequest<RepairDto>
    {
        public int RepairId;
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string CarWorkshopName { get; set; }
        public int VehicleId { get; set; }
    }

    public class UpdateRepairCommandHandler : IRequestHandler<UpdateRepairCommand, RepairDto>
    {
        private readonly IMapper mapper;
        private readonly VehicleProfileStorageContext context;

        public UpdateRepairCommandHandler(IMapper mapper, VehicleProfileStorageContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<RepairDto> Handle(UpdateRepairCommand request, CancellationToken cancellationToken)
        {
            var repair = await this.context.Repairs.FirstOrDefaultAsync(x => x.Id == request.RepairId);

            if (repair is null)
            {
                throw new NotFoundException("Repair not found.");
            }

            var updatedRepair = this.mapper.Map(request, repair);
            this.context.Repairs.Update(updatedRepair);
            await this.context.SaveChangesAsync();

            return this.mapper.Map<RepairDto>(updatedRepair);
        }
    }
}
