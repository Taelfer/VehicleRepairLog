using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Domain.Exceptions;
using VehicleRepairLog.Infrastructure;

namespace VehicleRepairLog.Application.Features.Parts
{
    public class DeletePartCommand : IRequest<PartDto>
    {
        public int PartId;
    }

    public class DeletePartCommandHandler : IRequestHandler<DeletePartCommand, PartDto>
    {
        private readonly IMapper mapper;
        private readonly VehicleProfileStorageContext context;

        public DeletePartCommandHandler(IMapper mapper, VehicleProfileStorageContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<PartDto> Handle(DeletePartCommand request, CancellationToken cancellationToken)
        {
            var part = await this.context.Parts.FirstOrDefaultAsync(x => x.Id == request.PartId);

            if (part is null)
            {
                throw new NotFoundException("Part not found.");
            }

            this.context.Parts.Remove(part);
            await this.context.SaveChangesAsync();

            return this.mapper.Map<PartDto>(part);
        }
    }
}
