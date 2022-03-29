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
    public class GetVehicleByIdHandler : IRequestHandler<GetVehicleByIdRequest, GetVehicleByIdResponse>
    {
        private readonly IMapper mapper;
        private readonly VehicleProfileStorageContext context;

        public GetVehicleByIdHandler(IMapper mapper, VehicleProfileStorageContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<GetVehicleByIdResponse> Handle(GetVehicleByIdRequest request, CancellationToken cancellationToken)
        {
            var vehicle = await context.Vehicles
                            .Include(x => x.Repairs)
                            .FirstOrDefaultAsync(x => x.Id == request.VehicleId);

            if (vehicle is null)
            {
                return new GetVehicleByIdResponse()
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            return new GetVehicleByIdResponse()
            {
                Data = this.mapper.Map<VehicleDto>(vehicle)
            };
        }
    }
}
