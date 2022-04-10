using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Infrastructure.Repositories;

namespace VehicleRepairLog.Application.Features.Repairs
{
    public class GetAllRepairsQuery : IRequest<List<RepairDto>>
    {
    }

    public class GetAllRepairsQueryHandler : IRequestHandler<GetAllRepairsQuery, List<RepairDto>>
    {
        private readonly IMapper mapper;
        private readonly IRepairRepository repairRepository;

        public GetAllRepairsQueryHandler(IMapper mapper, IRepairRepository repairRepository)
        {
            this.mapper = mapper;
            this.repairRepository = repairRepository;
        }

        public async Task<List<RepairDto>> Handle(GetAllRepairsQuery request, CancellationToken cancellationToken)
        {
            var repairs = await this.repairRepository.GetAllAsync();

            if (repairs is null)
            {
                throw new NotFoundException("Repairs not found.");
            }

            return this.mapper.Map<List<RepairDto>>(repairs);
        }
    }
}
