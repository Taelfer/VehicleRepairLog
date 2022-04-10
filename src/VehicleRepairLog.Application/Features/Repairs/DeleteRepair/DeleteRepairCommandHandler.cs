using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Infrastructure.Repositories;

namespace VehicleRepairLog.Application.Features.Repairs
{
    public class DeleteRepairCommand : IRequest<RepairDto>
    {
        public int RepairId;
    }

    public class DeleteRepairCommandHandler : IRequestHandler<DeleteRepairCommand, RepairDto>
    {
        private readonly IMapper mapper;
        private readonly IRepairRepository repairRepository;

        public DeleteRepairCommandHandler(IMapper mapper, IRepairRepository repairRepository)
        {
            this.mapper = mapper;
            this.repairRepository = repairRepository;
        }

        public async Task<RepairDto> Handle(DeleteRepairCommand request, CancellationToken cancellationToken)
        {
            var repair = await this.repairRepository.GetByIdAsync(request.RepairId);

            if (repair is null)
            {
                throw new NotFoundException("Repair not found.");
            }

            await this.repairRepository.RemoveAsync(repair);

            return this.mapper.Map<RepairDto>(repair);
        }
    }
}
