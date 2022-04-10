using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Infrastructure.Repositories;

namespace VehicleRepairLog.Application.Features.Parts
{
    public class GetPartByIdQuery : IRequest<PartDto>
    {
        public int PartId { get; set; }
    }

    public class GetPartByIdQueryHandler : IRequestHandler<GetPartByIdQuery, PartDto>
    {
        private readonly IMapper mapper;
        private readonly IPartRepository partRepository;

        public GetPartByIdQueryHandler(IMapper mapper, IPartRepository partRepository)
        {
            this.mapper = mapper;
            this.partRepository = partRepository;
        }

        public async Task<PartDto> Handle(GetPartByIdQuery request, CancellationToken cancellationToken)
        {
            var part = await this.partRepository.GetByIdAsync(request.PartId);

            if (part is null)
            {
                throw new NotFoundException("Part not found.");
            }

            return this.mapper.Map<PartDto>(part);
        }
    }
}
