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
    public class DeleteVehicleHandler : IRequestHandler<DeleteVehicleRequest, DeleteVehicleResponse>
    {
        private readonly IMapper mapper;
        private readonly VehicleProfileStorageContext context;

        public DeleteVehicleHandler(IMapper mapper, VehicleProfileStorageContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<DeleteVehicleResponse> Handle(DeleteVehicleRequest request, CancellationToken cancellationToken)
        {
            var vehicle = await this.context.Vehicles.FirstOrDefaultAsync(x => x.Id == request.VehicleId);

            if (vehicle is null)
            {
                return new DeleteVehicleResponse()
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            this.context.Vehicles.Remove(vehicle);
            await this.context.SaveChangesAsync();

            return new DeleteVehicleResponse()
            {
                Data = this.mapper.Map<VehicleDto>(vehicle)
            };
        }
    }
}
