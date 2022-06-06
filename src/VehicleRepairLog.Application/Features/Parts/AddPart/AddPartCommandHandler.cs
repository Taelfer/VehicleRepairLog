using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Infrastructure.Entities;
using VehicleRepairLog.Infrastructure.Repositories;

namespace VehicleRepairLog.Application.Features.Parts
{
    public class AddPartCommand : IRequest<PartDto>
    {
        public string Name { get; set; }
        public string BrandName { get; set; }
        public decimal Price { get; set; }
    }

    public class AddPartCommandHandler : IRequestHandler<AddPartCommand, PartDto>
    {
        private readonly IMapper _mapper;
        private readonly IPartRepository _partRepository;

        public AddPartCommandHandler(IMapper mapper, IPartRepository partRepository)
        {
            _mapper = mapper;
            _partRepository = partRepository;
        }

        public async Task<PartDto> Handle(AddPartCommand request, CancellationToken cancellationToken)
        {
            var part = _mapper.Map<Part>(request);

            await _partRepository.AddAsync(part);

            return _mapper.Map<PartDto>(part);
        }
    }
}
