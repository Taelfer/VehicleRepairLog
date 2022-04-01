using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Domain.Entities;
using VehicleRepairLog.Infrastructure;

namespace VehicleRepairLog.Application.Features.Parts
{
    public class GetAllPartsQuery : IRequest<List<PartDto>>
    {
        public string Name { get; set; }
    }

    public class GetAllPartsQueryHandler : IRequestHandler<GetAllPartsQuery, List<PartDto>>
    {
        private readonly IMapper mapper;
        private readonly IUserService userClaims;
        private readonly VehicleProfileStorageContext context;

        public GetAllPartsQueryHandler(IMapper mapper, IUserService userClaims, VehicleProfileStorageContext context)
        {
            this.mapper = mapper;
            this.userClaims = userClaims;
            this.context = context;
        }

        public async Task<List<PartDto>> Handle(GetAllPartsQuery request, CancellationToken cancellationToken)
        {
            var claims = userClaims.GetCurrentUser();
            List<Part> parts = null;

            if (claims.Role == "Admin")
            {
                parts = await this.context.Parts.ToListAsync();
            }
            else
            {
                return null;
            }

            if (parts is null)
            {
                return null;
            }

            return this.mapper.Map<List<PartDto>>(parts);
        }
    }
}
