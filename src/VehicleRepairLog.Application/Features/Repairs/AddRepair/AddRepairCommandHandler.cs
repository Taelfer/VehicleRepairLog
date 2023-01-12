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
    public class AddRepairCommand : IRequest<RepairDto>
    {
        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CarWorkshopName { get; set; }
        public int VehicleId { get; set; }
    }

    public class AddRepairCommandHandler : IRequestHandler<AddRepairCommand, RepairDto>
    {
        private readonly IMapper _mapper;
        private readonly VehicleRepairLogContext _context;

        public AddRepairCommandHandler(IMapper mapper, VehicleRepairLogContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<RepairDto> Handle(AddRepairCommand request, CancellationToken cancellationToken)
        {
            Vehicle vehicle = await _context.Vehicles.FirstOrDefaultAsync(vehicle => vehicle.Id == request.VehicleId);

            if (vehicle is null)
            {
                throw new NotFoundException("Couldn't find vehicle.");
            }

            Repair repair = _mapper.Map<Repair>(request);

            _context.Add(repair);
            await _context.SaveChangesAsync();

            return _mapper.Map<RepairDto>(repair);
        }
    }
}
