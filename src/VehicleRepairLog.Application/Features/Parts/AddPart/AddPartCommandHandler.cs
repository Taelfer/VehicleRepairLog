using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Infrastructure;
using VehicleRepairLog.Infrastructure.Entities;
using VehicleRepairLog.Shared.DtoModels;

namespace VehicleRepairLog.Application.Features.Parts
{
    public class AddPartCommand : IRequest<PartDto>
    {
        public string Name { get; set; }
        public string BrandName { get; set; }
        public decimal Price { get; set; }
        public short? Amount { get; set; }
        public int RepairId { get; set; }
    }

    public class AddPartCommandHandler : IRequestHandler<AddPartCommand, PartDto>
    {
        private readonly IMapper _mapper;
        private readonly VehicleRepairLogContext _context;

        public AddPartCommandHandler(IMapper mapper, VehicleRepairLogContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<PartDto> Handle(AddPartCommand request, CancellationToken cancellationToken)
        {
            Repair repair = await _context.Repairs.FirstOrDefaultAsync(repair => repair.Id == request.RepairId);

            if (repair is null)
            {
                throw new NotFoundException("Couldn't find repair.");
            }

            Part part = _mapper.Map<Part>(request);

            _context.Add(part);
            await _context.SaveChangesAsync();

            return _mapper.Map<PartDto>(part);
        }
    }
}
