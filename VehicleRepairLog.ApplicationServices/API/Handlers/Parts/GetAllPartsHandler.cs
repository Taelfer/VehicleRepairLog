using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain;
using VehicleRepairLog.ApplicationServices.API.Domain.Models;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Parts;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Parts;
using VehicleRepairLog.ApplicationServices.API.ErrorHandling;
using VehicleRepairLog.DataAccess;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Parts
{
    public class GetAllPartsHandler : IRequestHandler<GetAllPartsRequest, GetAllPartsResponse>
    {
        private readonly IMapper mapper;
        private readonly IUserService userClaims;
        private readonly VehicleProfileStorageContext context;

        public GetAllPartsHandler(IMapper mapper, IUserService userClaims, VehicleProfileStorageContext context)
        {
            this.mapper = mapper;
            this.userClaims = userClaims;
            this.context = context;
        }

        public async Task<GetAllPartsResponse> Handle(GetAllPartsRequest request, CancellationToken cancellationToken)
        {
            var claims = userClaims.GetCurrentUser();
            List<Part> parts = null;

            if (claims.Role == "Admin")
            {
                parts = await this.context.Parts.ToListAsync();
            }
            else
            {
                return new GetAllPartsResponse()
                {
                    Error = new ErrorModel(ErrorType.Unauthorized)
                };
            }

            if (parts is null)
            {
                return new GetAllPartsResponse()
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            return new GetAllPartsResponse()
            {
                Data = this.mapper.Map<List<PartDto>>(parts)
            };
        }
    }
}
