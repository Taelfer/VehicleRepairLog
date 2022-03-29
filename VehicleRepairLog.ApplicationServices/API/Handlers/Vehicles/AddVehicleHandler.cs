using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain.Models;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Vehicles;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Vehicles;
using VehicleRepairLog.DataAccess;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Vehicles
{
    public class AddVehicleHandler : IRequestHandler<AddVehicleRequest, AddVehicleResponse>
    {
        private readonly IMapper mapper;
        private readonly VehicleProfileStorageContext context;

        public AddVehicleHandler(IMapper mapper, VehicleProfileStorageContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<AddVehicleResponse> Handle(AddVehicleRequest request, CancellationToken cancellationToken)
        {
            var vehicle = this.mapper.Map<Vehicle>(request);

            this.context.Vehicles.Add(vehicle);
            await context.SaveChangesAsync();

            return new AddVehicleResponse()
            {
                Data = this.mapper.Map<VehicleDto>(vehicle)
            };
        }
    }
}
