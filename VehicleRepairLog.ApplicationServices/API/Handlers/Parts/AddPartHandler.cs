using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain.Models;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Parts;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Parts;
using VehicleRepairLog.DataAccess;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Parts
{
    public class AddPartHandler : IRequestHandler<AddPartRequest, AddPartResponse>
    {
        private readonly IMapper mapper;
        private readonly VehicleProfileStorageContext context;

        public AddPartHandler(IMapper mapper, VehicleProfileStorageContext context)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<AddPartResponse> Handle(AddPartRequest request, CancellationToken cancellationToken)
        {
            var part = this.mapper.Map<Part>(request);

            this.context.Parts.Add(part);
            await this.context.SaveChangesAsync();

            return new AddPartResponse()
            {
                Data = this.mapper.Map<PartDto>(part)
            };
        }
    }
}
