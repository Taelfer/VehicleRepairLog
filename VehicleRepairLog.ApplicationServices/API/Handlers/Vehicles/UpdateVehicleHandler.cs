using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain;
using VehicleRepairLog.ApplicationServices.API.Domain.Models;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Vehicles;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Vehicles;
using VehicleRepairLog.ApplicationServices.API.ErrorHandling;
using VehicleRepairLog.DataAccess;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Vehicles
{
    public class UpdateVehicleHandler : IRequestHandler<UpdateVehicleRequest, UpdateVehicleResponse>
    {
        private readonly IMapper mapper;
        private readonly VehicleProfileStorageContext context;

        public UpdateVehicleHandler(IMapper mapper, VehicleProfileStorageContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<UpdateVehicleResponse> Handle(UpdateVehicleRequest request, CancellationToken cancellationToken)
        {
            var vehicle = await this.context.Vehicles.FirstOrDefaultAsync(x => x.Id == request.VehicleId);

            if (vehicle is null)
            {
                return new UpdateVehicleResponse()
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            var updatedVehicle = this.mapper.Map(request, vehicle);
            this.context.Vehicles.Update(updatedVehicle);
            await this.context.SaveChangesAsync();

            return new UpdateVehicleResponse()
            {
                Data = this.mapper.Map<VehicleDto>(vehicle)
            };
        }
    }
}
