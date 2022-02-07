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

        public GetAllVehiclesHandler(IRepository<Vehicle> vehicleRepository)
        {
            this.vehicleRepository = vehicleRepository;
        }

        public Task<GetAllVehiclesResponse> Handle(GetAllVehiclesRequest request, CancellationToken cancellationToken)
        {
            var vehicles = this.vehicleRepository.GetAll();
            var vehiclesModel = vehicles.Select(x => new Domain.Models.Vehicle
            {
                Id = x.Id,
                BrandName = x.BrandName,
                VinNumber = x.VinNumber,
                PaintColor = x.PaintColor,
                FuelType = x.FuelType,
                Mileage = x.Mileage
            });

            var response = new GetAllVehiclesResponse()
            {
                Data = vehiclesModel.ToList()
            };

            return Task.FromResult(response);
        }
    }
}
