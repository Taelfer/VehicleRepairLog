using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Infrastructure.Repositories;

namespace VehicleRepairLog.Application.Features.Repairs
{
    public class GetRepairByIdQuery : IRequest<RepairDto>
    {
        public int RepairId;
    }

    public class GetRepairByIdQueryHandler : IRequestHandler<GetRepairByIdQuery, RepairDto>
    {
        private readonly IMapper mapper;
        private readonly IRepairRepository repairRepository;

        public GetRepairByIdQueryHandler(IMapper mapper, IRepairRepository repairRepository)
        {
            this.mapper = mapper;
            this.repairRepository = repairRepository;
        }

        public async Task<RepairDto> Handle(GetRepairByIdQuery request, CancellationToken cancellationToken)
        {
            var repair = await this.repairRepository.GetByIdAsync(request.RepairId);

            if (repair is null)
            {
                throw new NotFoundException("Repair not found.");
            }

            return this.mapper.Map<RepairDto>(repair);
        }
    }
}
