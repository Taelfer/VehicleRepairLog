using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Infrastructure.Entities;
using VehicleRepairLog.Infrastructure.Repositories;
using VehicleRepairLog.Shared.DtoModels;

namespace VehicleRepairLog.Application.Features.Repairs
{
    public class GetRepairByIdQuery : IRequest<RepairDto>
    {
        public int RepairId;
    }

    public class GetRepairByIdQueryHandler : IRequestHandler<GetRepairByIdQuery, RepairDto>
    {
        private readonly IMapper _mapper;
        private readonly IRepairRepository _repairRepository;

        public GetRepairByIdQueryHandler(IMapper mapper, IRepairRepository repairRepository)
        {
            _mapper = mapper;
            _repairRepository = repairRepository;
        }

        public async Task<RepairDto> Handle(GetRepairByIdQuery request, CancellationToken cancellationToken)
        {
            Repair repair = await _repairRepository.GetByIdAsync(request.RepairId);

            if (repair is null)
            {
                throw new NotFoundException("Repair not found.");
            }

            return _mapper.Map<RepairDto>(repair);
        }
    }
}
