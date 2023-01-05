using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Infrastructure.Entities;
using VehicleRepairLog.Infrastructure.Repositories;
using VehicleRepairLog.Shared.DtoModels;

namespace VehicleRepairLog.Application.Features.Parts
{
    public class GetPartByIdQuery : IRequest<PartDto>
    {
        public int PartId { get; set; }
    }

    public class GetPartByIdQueryHandler : IRequestHandler<GetPartByIdQuery, PartDto>
    {
        private readonly IMapper _mapper;
        private readonly IPartRepository _partRepository;

        public GetPartByIdQueryHandler(IMapper mapper, IPartRepository partRepository)
        {
            _mapper = mapper;
            _partRepository = partRepository;
        }

        public async Task<PartDto> Handle(GetPartByIdQuery request, CancellationToken cancellationToken)
        {
            Part part = await _partRepository.GetByIdAsync(request.PartId);

            if (part is null)
            {
                throw new NotFoundException("Part not found.");
            }

            return _mapper.Map<PartDto>(part);
        }
    }
}
