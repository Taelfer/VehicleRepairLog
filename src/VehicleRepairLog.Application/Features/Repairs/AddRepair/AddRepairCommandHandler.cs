using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Infrastructure.Entities;
using VehicleRepairLog.Infrastructure.Repositories;

namespace VehicleRepairLog.Application.Features.Repairs
{
    public class AddRepairCommand : IRequest<RepairDto>
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string CarWorkshopName { get; set; }
        public int VehicleId { get; set; }
        public List<string> PartNames { get; set; }
    }

    public class AddRepairCommandHandler : IRequestHandler<AddRepairCommand, RepairDto>
    {
        private readonly IMapper _mapper;
        private readonly IRepairRepository _repairRepository;
        private readonly IPartRepository _partRepository;

        public AddRepairCommandHandler(IMapper mapper, IRepairRepository repairRepository, IPartRepository partRepository)
        {
            _mapper = mapper;
            _repairRepository = repairRepository;
            _partRepository = partRepository;
        }

        public async Task<RepairDto> Handle(AddRepairCommand request, CancellationToken cancellationToken)
        {
            var repair = _mapper.Map<Repair>(request);

            List<Part> parts = await _partRepository.GetByNameAsync(request.PartNames);
            repair.Parts = parts;

            await _repairRepository.AddAsync(repair, request.PartNames);

            return _mapper.Map<RepairDto>(repair);
        }
    }
}
