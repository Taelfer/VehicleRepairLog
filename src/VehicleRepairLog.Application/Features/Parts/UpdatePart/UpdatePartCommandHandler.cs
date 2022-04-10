using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Infrastructure.Repositories;

namespace VehicleRepairLog.Application.Features.Parts
{
    public class UpdatePartCommand : IRequest<PartDto>
    {
        public int PartId;
        public string Name { get; set; }
        public string BrandName { get; set; }
        public decimal Price { get; set; }
    }

    public class UpdatePartCommandHandler : IRequestHandler<UpdatePartCommand, PartDto>
    {
        private readonly IMapper mapper;
        private readonly IPartRepository partRepository;

        public UpdatePartCommandHandler(IMapper mapper, IPartRepository partRepository)
        {
            this.mapper = mapper;
            this.partRepository = partRepository;
        }

        public async Task<PartDto> Handle(UpdatePartCommand request, CancellationToken cancellationToken)
        {
            var part = await this.partRepository.GetByIdAsync(request.PartId);

            if (part is null)
            {
                throw new NotFoundException("Part not found.");
            }

            this.mapper.Map(request, part);

            await this.partRepository.UpdateAsync(part);

            return this.mapper.Map<PartDto>(part);
        }
    }
}
