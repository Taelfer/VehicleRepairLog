using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Infrastructure;
using VehicleRepairLog.Infrastructure.Entities;
using VehicleRepairLog.Shared.DtoModels;

namespace VehicleRepairLog.Application.Features.Repairs
{
    public class DeleteRepairCommand : IRequest<RepairDto>
    {
        public int RepairId;
    }

    public class DeleteRepairCommandHandler : IRequestHandler<DeleteRepairCommand, RepairDto>
    {
        private readonly IMapper _mapper;
        private readonly VehicleRepairLogContext _context;

        public DeleteRepairCommandHandler(IMapper mapper, VehicleRepairLogContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<RepairDto> Handle(DeleteRepairCommand request, CancellationToken cancellationToken)
        {
            Repair repair = await _context.Repairs.FirstOrDefaultAsync(repair => repair.Id == request.RepairId);

            if (repair is null)
            {
                throw new NotFoundException("Repair has not been found.");
            }

            _context.Repairs.Remove(repair);
            await _context.SaveChangesAsync();

            return _mapper.Map<RepairDto>(repair);
        }
    }
}
