using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Infrastructure.Entities;
using VehicleRepairLog.Infrastructure.Repositories;

namespace VehicleRepairLog.Application.Features.Repairs
{
    public class GetAllRepairsQuery : IRequest<List<RepairDto>>
    {
    }

    public class GetAllRepairsQueryHandler : IRequestHandler<GetAllRepairsQuery, List<RepairDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepairRepository _repairRepository;

        public GetAllRepairsQueryHandler(IMapper mapper, IRepairRepository repairRepository)
        {
            _mapper = mapper;
            _repairRepository = repairRepository;
        }

        public async Task<List<RepairDto>> Handle(GetAllRepairsQuery request, CancellationToken cancellationToken)
        {
            List<Repair> repairs = await _repairRepository.GetAllAsync();

            if (repairs is null)
            {
                throw new NotFoundException("Repairs not found.");
            }

            return _mapper.Map<List<RepairDto>>(repairs);
        }
    }
}
