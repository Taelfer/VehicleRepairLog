using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Repairs;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Repairs;
using VehicleRepairLog.DataAccess;
using VehicleRepairLog.DataAccess.Entites;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Repairs
{
    public class GetAllRepairsHandler : IRequestHandler<GetAllRepairsRequest, GetAllRepairsResponse>
    {
        private readonly IRepository<Repair> repairRepository;
        private readonly IMapper mapper;

        public GetAllRepairsHandler(IRepository<Repair> repairRepository, IMapper mapper)
        {
            this.repairRepository = repairRepository;
            this.mapper = mapper;
        }

        public async Task<GetAllRepairsResponse> Handle(GetAllRepairsRequest request, CancellationToken cancellationToken)
        {
            var repairs = await this.repairRepository.GetAll();
            var mappedRepairs = this.mapper.Map <List<Domain.Models.Repair>>(repairs);

            var response = new GetAllRepairsResponse()
            {
                Data = mappedRepairs
            };

            return response;
        }
    }
}
