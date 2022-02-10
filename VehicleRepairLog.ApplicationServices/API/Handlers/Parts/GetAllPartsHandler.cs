using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Parts;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Parts;
using VehicleRepairLog.DataAccess;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Parts
{
    public class GetAllPartsHandler : IRequestHandler<GetAllPartsRequest, GetAllPartsResponse>
    {
        private readonly IRepository<DataAccess.Entites.Part> partsRepository;
        private readonly IMapper mapper;

        public GetAllPartsHandler(IRepository<DataAccess.Entites.Part> partsRepository, IMapper mapper)
        {
            this.partsRepository = partsRepository;
            this.mapper = mapper;
        }

        public async Task<GetAllPartsResponse> Handle(GetAllPartsRequest request, CancellationToken cancellationToken)
        {
            var parts = await this.partsRepository.GetAll();
            var mappedParts = this.mapper.Map<List<Domain.Models.Part>>(parts);

            var response = new GetAllPartsResponse()
            {
                Data = mappedParts.ToList()
            };

            return response;
        }
    }
}
