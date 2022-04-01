using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Domain.Entities;
using VehicleRepairLog.Infrastructure;

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
        private readonly VehicleProfileStorageContext context;

        public AddPartCommandHandler(IMapper mapper, VehicleProfileStorageContext context)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<PartDto> Handle(AddPartCommand request, CancellationToken cancellationToken)
        {
            var part = this.mapper.Map<Part>(request);

            this.context.Parts.Add(part);
            await this.context.SaveChangesAsync();

            return this.mapper.Map<PartDto>(part);
        }
    }
}
