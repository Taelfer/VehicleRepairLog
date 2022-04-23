using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Authentication;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Infrastructure.Entities;
using VehicleRepairLog.Infrastructure.Repositories;

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
        private readonly IPartRepository partRepository;

        public GetAllPartsQueryHandler(IMapper mapper, IUserService userClaims, IPartRepository partRepository)
        {
            this.mapper = mapper;
            this.userClaims = userClaims;
            this.partRepository = partRepository;
        }

        public async Task<List<PartDto>> Handle(GetAllPartsQuery request, CancellationToken cancellationToken)
        {
            var claims = userClaims.GetCurrentUser();
            List<Part> parts = null;

            if (claims.Role == "Admin")
            {
                parts = await this.partRepository.GetAllAsync();
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
