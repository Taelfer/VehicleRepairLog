using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Infrastructure.Entities;
using VehicleRepairLog.Application.Exceptions;
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
                throw new UnauthorizedException("You have no access to this resource.");
            }

            if (parts is null)
            {
                throw new NotFoundException("Parts not found.");
            }

            return this.mapper.Map<List<PartDto>>(parts);
        }
    }
}
