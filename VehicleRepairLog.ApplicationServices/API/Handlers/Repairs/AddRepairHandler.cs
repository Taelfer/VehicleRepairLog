using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain.Models;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Repairs;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Repairs;
using VehicleRepairLog.DataAccess;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Repairs
{
    public class AddRepairHandler : IRequestHandler<AddRepairRequest, AddRepairResponse>
    {
        private readonly IMapper mapper;
        private readonly VehicleProfileStorageContext context;

        public AddRepairHandler(IMapper mapper, VehicleProfileStorageContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<AddRepairResponse> Handle(AddRepairRequest request, CancellationToken cancellationToken)
        {
            var repair = this.mapper.Map<Repair>(request);
            var parts = await this.context.Parts.Where(x => request.PartNames.Contains(x.Name)).ToListAsync();
            repair.Parts = parts;
            
            this.context.Repairs.Add(repair);
            await this.context.SaveChangesAsync();

            return new AddRepairResponse()
            {
                Data = this.mapper.Map<RepairDto>(repair)
            };
        }
    }
}
