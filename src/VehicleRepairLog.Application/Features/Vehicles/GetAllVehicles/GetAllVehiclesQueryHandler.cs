using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Authentication;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Infrastructure;
using VehicleRepairLog.Infrastructure.Entities;

namespace VehicleRepairLog.Application.Features.Vehicles
{
    public class GetAllVehiclesQuery : IRequest<List<VehicleDto>>
    {
    }

    public class GetAllVehiclesQueryHandler : IRequestHandler<GetAllVehiclesQuery, List<VehicleDto>>
    {
        private readonly IMapper _mapper;
        private readonly VehicleProfileStorageContext _context;
        private readonly IUserService _userService;

        public GetAllVehiclesQueryHandler(IMapper mapper, VehicleProfileStorageContext context, IUserService userService)
        {
            _mapper = mapper;
            _context = context;
            _userService = userService;
        }

        public async Task<List<VehicleDto>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
        {
            //if (cancellationToken is null)
            //{
            //    throw new TaskCanceledException("Task canceled");
            //}

            int? userId = _userService.GetCurrentUserId();

            List<Vehicle> vehicles = await _context.Vehicles.Where(vehicle => vehicle.UserId == userId)
                                                            .Include(vehicle => vehicle.Repairs)
                                                            .ToListAsync(cancellationToken);

            if (vehicles is null)
            {
                throw new NotFoundException("Vehicles not found.");
            }

            return _mapper.Map<List<VehicleDto>>(vehicles);
        }
    }
}
