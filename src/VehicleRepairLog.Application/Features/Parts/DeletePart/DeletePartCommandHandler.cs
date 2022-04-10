using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Infrastructure.Repositories;

namespace VehicleRepairLog.Application.Features.Parts
{
    public class DeletePartCommand : IRequest<PartDto>
    {
        public int PartId;
    }

    public class DeletePartCommandHandler : IRequestHandler<DeletePartCommand, PartDto>
    {
        private readonly IMapper mapper;
        private readonly IPartRepository partRepository;

        public DeletePartCommandHandler(IMapper mapper, IPartRepository partRepository)
        {
            this.mapper = mapper;
            this.partRepository = partRepository;
        }

        public async Task<PartDto> Handle(DeletePartCommand request, CancellationToken cancellationToken)
        {
            var part = await this.partRepository.GetByIdAsync(request.PartId);

            if (part is null)
            {
                throw new NotFoundException("Part not found.");
            }

            await this.partRepository.RemoveAsync(part);

            return this.mapper.Map<PartDto>(part);
        }
    }
}
