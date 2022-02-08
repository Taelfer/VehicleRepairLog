using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Vehicles;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Vehicles;
using VehicleRepairLog.DataAccess;
using VehicleRepairLog.DataAccess.Entites;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Vehicles
{
    public class GetAllVehiclesHandler : IRequestHandler<GetAllVehiclesRequest, GetAllVehiclesResponse>
    {
        private readonly IRepository<Vehicle> vehicleRepository;
        private readonly IMapper mapper;

        public GetAllVehiclesHandler(IRepository<Vehicle> vehicleRepository, IMapper mapper)
        {
            this.vehicleRepository = vehicleRepository;
            this.mapper = mapper;
        }

        public Task<GetAllVehiclesResponse> Handle(GetAllVehiclesRequest request, CancellationToken cancellationToken)
        {
            var vehicles = this.vehicleRepository.GetAll();
            var mappedVehicles = this.mapper.Map<List<Domain.Models.Vehicle>>(vehicles);

            var response = new GetAllVehiclesResponse()
            {
                Data = mappedVehicles
            };

            return Task.FromResult(response);
        }
    }
}
