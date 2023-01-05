using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Infrastructure.Entities;
using VehicleRepairLog.Infrastructure.Repositories;
using VehicleRepairLog.Shared.DtoModels;

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
        private readonly IMapper _mapper;
        private readonly IPartRepository _partRepository;

        public UpdatePartCommandHandler(IMapper mapper, IPartRepository partRepository)
        {
            _mapper = mapper;
            _partRepository = partRepository;
        }

        public async Task<PartDto> Handle(UpdatePartCommand request, CancellationToken cancellationToken)
        {
            Part part = await _partRepository.GetByIdAsync(request.PartId);

            if (part is null)
            {
                throw new NotFoundException("Part not found.");
            }

            _mapper.Map(request, part);

            await _partRepository.UpdateAsync(part);

            return _mapper.Map<PartDto>(part);
        }
    }
}
