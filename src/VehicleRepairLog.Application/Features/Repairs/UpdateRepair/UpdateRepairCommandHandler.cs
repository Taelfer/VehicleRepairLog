using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Infrastructure.Entities;
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
        private readonly IMapper _mapper;
        private readonly IRepairRepository _repairRepository;

        public UpdateRepairCommandHandler(IMapper mapper, IRepairRepository repairRepository)
        {
            _mapper = mapper;
            _repairRepository = repairRepository;
        }

        public async Task<RepairDto> Handle(UpdateRepairCommand request, CancellationToken cancellationToken)
        {
            Repair repair = await _repairRepository.GetByIdAsync(request.RepairId);

            if (repair is null)
            {
                throw new NotFoundException("Repair not found.");
            }

            Repair updatedRepair = _mapper.Map(request, repair);

            await _repairRepository.UpdateAsync(repair);

            return _mapper.Map<RepairDto>(updatedRepair);
        }
    }
}
