using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Infrastructure.Entities;
using VehicleRepairLog.Infrastructure.Repositories;

namespace VehicleRepairLog.Application.Features.Repairs
{
    public class DeleteRepairCommand : IRequest<RepairDto>
    {
        public int RepairId;
    }

    public class DeleteRepairCommandHandler : IRequestHandler<DeleteRepairCommand, RepairDto>
    {
        private readonly IMapper _mapper;
        private readonly IRepairRepository _repairRepository;

        public DeleteRepairCommandHandler(IMapper mapper, IRepairRepository repairRepository)
        {
            _mapper = mapper;
            _repairRepository = repairRepository;
        }

        public async Task<RepairDto> Handle(DeleteRepairCommand request, CancellationToken cancellationToken)
        {
            Repair repair = await _repairRepository.GetByIdAsync(request.RepairId);

            if (repair is null)
            {
                throw new NotFoundException("Repair not found.");
            }

            await _repairRepository.RemoveAsync(repair);

            return _mapper.Map<RepairDto>(repair);
        }
    }
}
