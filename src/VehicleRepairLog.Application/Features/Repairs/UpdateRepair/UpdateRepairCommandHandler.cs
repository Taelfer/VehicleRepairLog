using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Infrastructure.Repositories;

namespace VehicleRepairLog.Application.Features.Repairs
{
    public class UpdateRepairCommand : IRequest<RepairDto>
    {
        public int RepairId;
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string CarWorkshopName { get; set; }
        public int VehicleId { get; set; }
    }

    public class UpdateRepairCommandHandler : IRequestHandler<UpdateRepairCommand, RepairDto>
    {
        private readonly IMapper mapper;
        private readonly IRepairRepository repairRepository;

        public UpdateRepairCommandHandler(IMapper mapper, IRepairRepository repairRepository)
        {
            this.mapper = mapper;
            this.repairRepository = repairRepository;
        }

        public async Task<RepairDto> Handle(UpdateRepairCommand request, CancellationToken cancellationToken)
        {
            var repair = await this.repairRepository.GetByIdAsync(request.RepairId);

            if (repair is null)
            {
                throw new NotFoundException("Repair not found.");
            }

            var updatedRepair = this.mapper.Map(request, repair);

            await this.repairRepository.UpdateAsync(repair);

            return this.mapper.Map<RepairDto>(updatedRepair);
        }
    }
}
