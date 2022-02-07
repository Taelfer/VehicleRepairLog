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

        public GetAllRepairsHandler(IRepository<Repair> repairRepository)
        {
            this.repairRepository = repairRepository;
        }

        public Task<GetAllRepairsResponse> Handle(GetAllRepairsRequest request, CancellationToken cancellationToken)
        {
            var repairs = this.repairRepository.GetAll();
            var repairsModel = repairs.Select(x => new Domain.Models.Repair
            {
                Date = x.Date,
                Description = x.Description,
                CarWorkshopName = x.CarWorkshopName
            });

            var response = new GetAllRepairsResponse()
            {
                Data = repairsModel.ToList()
            };

            return Task.FromResult(response);
        }
    }
}
