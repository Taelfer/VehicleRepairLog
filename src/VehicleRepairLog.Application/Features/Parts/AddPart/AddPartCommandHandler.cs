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
        private readonly IMapper mapper;
        private readonly IPartRepository partRepository;

        public AddPartCommandHandler(IMapper mapper, IPartRepository partRepository)
        {
            this.mapper = mapper;
            this.partRepository = partRepository;
        }

        public async Task<PartDto> Handle(AddPartCommand request, CancellationToken cancellationToken)
        {
            var part = this.mapper.Map<Part>(request);

            await this.partRepository.AddAsync(part);

            return this.mapper.Map<PartDto>(part);
        }
    }
}
