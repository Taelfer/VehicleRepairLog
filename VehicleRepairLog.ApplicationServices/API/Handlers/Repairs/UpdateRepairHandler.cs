using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain;
using VehicleRepairLog.ApplicationServices.API.Domain.Models;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Repairs;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Repairs;
using VehicleRepairLog.ApplicationServices.API.ErrorHandling;
using VehicleRepairLog.DataAccess;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Repairs
{
    public class UpdateRepairHandler : IRequestHandler<UpdateRepairRequest, UpdateRepairResponse>
    {
        private readonly IMapper mapper;
        private readonly VehicleProfileStorageContext context;

        public UpdateRepairHandler(IMapper mapper, VehicleProfileStorageContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<UpdateRepairResponse> Handle(UpdateRepairRequest request, CancellationToken cancellationToken)
        {
            var repair = await this.context.Repairs.FirstOrDefaultAsync(x => x.Id == request.RepairId);

            if (repair is null)
            {
                return new UpdateRepairResponse()
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            var updatedRepair = this.mapper.Map(request, repair);
            this.context.Repairs.Update(updatedRepair);
            await this.context.SaveChangesAsync();

            return new UpdateRepairResponse()
            {
                Data = this.mapper.Map<RepairDto>(updatedRepair)
            };
        }
    }
}
