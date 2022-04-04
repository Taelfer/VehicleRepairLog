using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Infrastructure;

namespace VehicleRepairLog.Application.Features.Repairs
{
    public class DeleteRepairCommand : IRequest<RepairDto>
    {
        public int RepairId;
    }

    public class DeleteRepairCommandHandler : IRequestHandler<DeleteRepairCommand, RepairDto>
    {
        private readonly IMapper mapper;
        private readonly VehicleProfileStorageContext context;

        public DeleteRepairCommandHandler(IMapper mapper, VehicleProfileStorageContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<RepairDto> Handle(DeleteRepairCommand request, CancellationToken cancellationToken)
        {
            var repair = await this.context.Repairs.FirstOrDefaultAsync(x => x.Id == request.RepairId);

            if (repair is null)
            {
                throw new NotFoundException("Repair not found.");
            }

            this.context.Repairs.Remove(repair);
            await this.context.SaveChangesAsync();

            return this.mapper.Map<RepairDto>(repair);
        }
    }
}
