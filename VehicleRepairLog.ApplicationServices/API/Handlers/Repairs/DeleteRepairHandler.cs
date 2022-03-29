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
    public class DeleteRepairHandler : IRequestHandler<DeleteRepairRequest, DeleteRepairResponse>
    {
        private readonly IMapper mapper;
        private readonly VehicleProfileStorageContext context;

        public DeleteRepairHandler(IMapper mapper, VehicleProfileStorageContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<DeleteRepairResponse> Handle(DeleteRepairRequest request, CancellationToken cancellationToken)
        {
            var repair = await this.context.Repairs.FirstOrDefaultAsync(x => x.Id == request.RepairId);

            if (repair is null)
            {
                return new DeleteRepairResponse()
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            this.context.Repairs.Remove(repair);
            await this.context.SaveChangesAsync();

            return new DeleteRepairResponse()
            {
                Data = this.mapper.Map<RepairDto>(repair)
            };
        }
    }
}
