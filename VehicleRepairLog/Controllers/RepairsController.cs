using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Repairs;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Repairs;

namespace VehicleRepairLog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RepairsController : ApiControllerBase
    {
        public RepairsController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        [Route("")]
        public Task<IActionResult> AddRepair([FromBody] AddRepairRequest request)
        {
            return this.HandleRequest<AddRepairRequest, AddRepairResponse>(request);
        }

        [HttpGet]
        [Route("")]
        public Task<IActionResult> GetAllRepairs([FromQuery] GetAllRepairsRequest request)
        {
            return this.HandleRequest<GetAllRepairsRequest, GetAllRepairsResponse>(request);
        }

        [HttpGet]
        [Route("{repairId}")]
        public Task<IActionResult> GetRepairById([FromRoute] int repairId)
        {
            var request = new GetRepairByIdRequest()
            {
                RepairId = repairId
            };

            return this.HandleRequest<GetRepairByIdRequest, GetRepairByIdResponse>(request);
        }

        [HttpPut]
        [Route("{repairId}")]
        public Task<IActionResult> UpdateRepair([FromBody] UpdateRepairRequest request, int repairId)
        {
            request.RepairId = repairId;

            return this.HandleRequest<UpdateRepairRequest, UpdateRepairResponse>(request);
        }

        [HttpDelete]
        [Route("{repairId}")]
        public Task<IActionResult> DeleteRepair([FromRoute] int repairId)
        {
            var request = new DeleteRepairRequest()
            {
                RepairId = repairId
            };

            return this.HandleRequest<DeleteRepairRequest, DeleteRepairResponse>(request);
        }
    }
}
