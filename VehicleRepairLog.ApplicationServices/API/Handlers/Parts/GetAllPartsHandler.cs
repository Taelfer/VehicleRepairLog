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
        private readonly DataAccess.IRepository<DataAccess.Entites.Part> partsRepository;

        public GetAllPartsHandler(IRepository<DataAccess.Entites.Part> partsRepository)
        {
            this.partsRepository = partsRepository;
        }

        public Task<GetAllPartsResponse> Handle(GetAllPartsRequest request, CancellationToken cancellationToken)
        {
            var parts = partsRepository.GetAll();
            var partsModel = parts.Select(x => new Domain.Models.Part()
            {
                Id = x.Id,
                Name = x.Name,
                BrandName = x.BrandName,
                Price = x.Price
            });

            var response = new GetAllPartsResponse()
            {
                Data = partsModel.ToList()
            };

            return Task.FromResult(response);
        }
    }
}
