using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Infrastructure;
using VehicleRepairLog.Infrastructure.Entities;
using VehicleRepairLog.Shared.DtoModels;

namespace VehicleRepairLog.Application.Features.Vehicles
{
    public class AddVehicleCommand : IRequest<VehicleDto>
    {
        public string Name { get; set; }
        public string BrandName { get; set; }
        public string VinNumber { get; set; }
        public string PaintColor { get; set; }
        public string FuelType { get; set; }
        public int Mileage { get; set; }
        public int UserId { get; set; }
    }

    public class AddVehicleCommandHandler : IRequestHandler<AddVehicleCommand, VehicleDto>
    {
        private readonly IMapper _mapper;
        private readonly VehicleRepairLogContext _context;

        public AddVehicleCommandHandler(IMapper mapper, VehicleRepairLogContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<VehicleDto> Handle(AddVehicleCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == request.UserId);

            if (user is null)
            {
                throw new NotFoundException("Couldn't find user.");
            }

            var vehicle = _mapper.Map<Vehicle>(request);

            _context.Add(vehicle);
            await _context.SaveChangesAsync();

            return _mapper.Map<VehicleDto>(vehicle);
        }
    }
}
