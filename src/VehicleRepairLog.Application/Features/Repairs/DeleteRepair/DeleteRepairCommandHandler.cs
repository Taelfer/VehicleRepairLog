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
            Repair repair = null;

            repair = await _repairRepository.GetByIdAsync(request.RepairId);

            if (repair is null)
            {
                throw new NotFoundException("Repair has not been found.");
            }

            await _repairRepository.RemoveAsync(repair);

            return _mapper.Map<RepairDto>(repair);
        }
    }
}
