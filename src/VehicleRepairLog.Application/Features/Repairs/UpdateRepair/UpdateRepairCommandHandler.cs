using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Infrastructure;
using VehicleRepairLog.Infrastructure.Entities;
using VehicleRepairLog.Shared.DtoModels;

namespace VehicleRepairLog.Application.Features.Repairs
{
    public class UpdateRepairCommand : IRequest<RepairDto>
    {
        public int RepairId;
        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CarWorkshopName { get; set; }
        public int VehicleId { get; set; }
    }

    public class UpdateRepairCommandHandler : IRequestHandler<UpdateRepairCommand, RepairDto>
    {
        private readonly IMapper _mapper;
        private readonly VehicleRepairLogContext _context;

        public UpdateRepairCommandHandler(IMapper mapper, VehicleRepairLogContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<RepairDto> Handle(UpdateRepairCommand request, CancellationToken cancellationToken)
        {
            Repair repair = await _context.Repairs.FirstOrDefaultAsync(repair => repair.Id == request.RepairId);

            if (repair is null)
            {
                throw new NotFoundException("Repair not found.");
            }

            Repair updatedRepair = _mapper.Map(request, repair);
            _context.Repairs.Update(updatedRepair);
            await _context.SaveChangesAsync();

            return _mapper.Map<RepairDto>(updatedRepair);
        }
    }
}
