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
        private readonly IMediator mediator;

        public RepairsController(IMediator mediator) : base(mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("")]
        public Task<IActionResult> AddRepair([FromBody] AddRepairRequest request)
        {
            return this.HandleRequest<AddRepairRequest, AddRepairResponse>(request);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllRepairs([FromQuery] GetAllRepairsRequest request)
        {
            var response = await this.mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]
        [Route("{repairId}")]
        public async Task<IActionResult> GetRepairById([FromRoute] int repairId)
        {
            var request = new GetRepairByIdRequest()
            {
                RepairId = repairId
            };

            var response = await this.mediator.Send(request);
            return this.Ok(response);
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
        public async Task<IActionResult> DeleteRepair([FromRoute] int repairId)
        {
            var request = new DeleteRepairRequest()
            {
                RepairId = repairId
            };

            var response = await this.mediator.Send(request);
            return this.Ok(response);
        }
    }
}
