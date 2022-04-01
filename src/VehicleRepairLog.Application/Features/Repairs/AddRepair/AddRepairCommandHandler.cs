using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Domain.Entities;
using VehicleRepairLog.Infrastructure;

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
        private readonly IMapper mapper;
        private readonly VehicleProfileStorageContext context;

        public AddRepairCommandHandler(IMapper mapper, VehicleProfileStorageContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<RepairDto> Handle(AddRepairCommand request, CancellationToken cancellationToken)
        {
            var repair = this.mapper.Map<Repair>(request);

            var parts = await this.context.Parts.Where(x => request.PartNames.Contains(x.Name)).ToListAsync();
            repair.Parts = parts;
            
            this.context.Repairs.Add(repair);
            await this.context.SaveChangesAsync();

            return this.mapper.Map<RepairDto>(repair);
        }
    }
}
