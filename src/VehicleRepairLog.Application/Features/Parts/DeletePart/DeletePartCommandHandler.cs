using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Infrastructure.Entities;
using VehicleRepairLog.Infrastructure.Repositories;

namespace VehicleRepairLog.Application.Features.Parts
{
    public class DeletePartCommand : IRequest<PartDto>
    {
        public int PartId;
    }

    public class DeletePartCommandHandler : IRequestHandler<DeletePartCommand, PartDto>
    {
        private readonly IMapper _mapper;
        private readonly IPartRepository _partRepository;

        public DeletePartCommandHandler(IMapper mapper, IPartRepository partRepository)
        {
            _mapper = mapper;
            _partRepository = partRepository;
        }

        public async Task<PartDto> Handle(DeletePartCommand request, CancellationToken cancellationToken)
        {
            Part part = await _partRepository.GetByIdAsync(request.PartId);

            if (part is null)
            {
                throw new NotFoundException("Part not found.");
            }

            await _partRepository.RemoveAsync(part);

            return _mapper.Map<PartDto>(part);
        }
    }
}
