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
        private readonly VehicleProfileStorageContext context;

        public UpdatePartCommandHandler(IMapper mapper, VehicleProfileStorageContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<PartDto> Handle(UpdatePartCommand request, CancellationToken cancellationToken)
        {
            var part = await this.context.Parts.FirstOrDefaultAsync(x => x.Id == request.PartId);

            if (part is null)
            {
                throw new NotFoundException("Part not found.");
            }

            this.mapper.Map(request, part);
            this.context.Parts.Update(part);
            await this.context.SaveChangesAsync();

            return this.mapper.Map<PartDto>(part);
        }
    }
}
