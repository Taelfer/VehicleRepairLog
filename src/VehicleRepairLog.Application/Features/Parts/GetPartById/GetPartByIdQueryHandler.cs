using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Infrastructure;

namespace VehicleRepairLog.Application.Features.Parts
{
    public class GetPartByIdQuery : IRequest<PartDto>
    {
        public int PartId { get; set; }
    }

    public class GetPartByIdQueryHandler : IRequestHandler<GetPartByIdQuery, PartDto>
    {
        private readonly IMapper mapper;
        private readonly VehicleProfileStorageContext context;

        public GetPartByIdQueryHandler(IMapper mapper, VehicleProfileStorageContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<PartDto> Handle(GetPartByIdQuery request, CancellationToken cancellationToken)
        {
            var part = await this.context.Parts.FirstOrDefaultAsync(x => x.Id == request.PartId);

            if (part is null)
            {
                throw new NotFoundException("Part not found.");
            }

            return this.mapper.Map<PartDto>(part);
        }
    }
}
