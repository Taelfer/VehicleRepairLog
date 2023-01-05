using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Authentication;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Infrastructure.Entities;
using VehicleRepairLog.Infrastructure.Repositories;
using VehicleRepairLog.Shared.DtoModels;

namespace VehicleRepairLog.Application.Features.Parts
{
    public class GetAllPartsQuery : IRequest<List<PartDto>>
    {
        public string Name { get; set; }
    }

    public class GetAllPartsQueryHandler : IRequestHandler<GetAllPartsQuery, List<PartDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userClaims;
        private readonly IPartRepository _partRepository;

        public GetAllPartsQueryHandler(IMapper mapper, IUserService userClaims, IPartRepository partRepository)
        {
            _mapper = mapper;
            _userClaims = userClaims;
            _partRepository = partRepository;
        }

        public async Task<List<PartDto>> Handle(GetAllPartsQuery request, CancellationToken cancellationToken)
        {
            string userRole = _userClaims.GetCurrentUserRole();
            List<Part> parts = null;

            if (userRole == "Admin")
            {
                parts = await _partRepository.GetAllAsync();
            }
            else
            {
                throw new UnauthorizedException("You have no access to this resource.");
            }

            if (parts is null)
            {
                throw new NotFoundException("Parts not found.");
            }

            return _mapper.Map<List<PartDto>>(parts);
        }
    }
}
