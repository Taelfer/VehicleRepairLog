using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain;
using VehicleRepairLog.ApplicationServices.API.Domain.Models;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Parts;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Parts;
using VehicleRepairLog.ApplicationServices.API.ErrorHandling;
using VehicleRepairLog.DataAccess;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Parts
{
    public class UpdatePartHandler : IRequestHandler<UpdatePartRequest, UpdatePartResponse>
    {
        private readonly IMapper mapper;
        private readonly VehicleProfileStorageContext context;

        public UpdatePartHandler(IMapper mapper, VehicleProfileStorageContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<UpdatePartResponse> Handle(UpdatePartRequest request, CancellationToken cancellationToken)
        {
            var part = await this.context.Parts.FirstOrDefaultAsync(x => x.Id == request.PartId);

            if (part is null)
            {
                return new UpdatePartResponse()
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }
            this.mapper.Map(request, part);
            this.context.Parts.Update(part);
            await this.context.SaveChangesAsync();

            return new UpdatePartResponse()
            {
                Data = this.mapper.Map<PartDto>(part)
            };
        }
    }
}
