using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Features.Repairs;

namespace VehicleRepairLog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairsController : ControllerBase
    {
        private readonly IMediator mediator;

        public RepairsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddRepair([FromBody] AddRepairCommand command)
        {
            await this.mediator.Send(command);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRepairs([FromQuery] GetAllRepairsQuery query)
        {
            var response = await this.mediator.Send(query);
            return this.Ok(response);
        }

        [HttpGet]
        [Route("{repairId}")]
        public async Task<IActionResult> GetRepairById([FromRoute] int repairId)
        {
            var query = new GetRepairByIdQuery()
            {
                RepairId = repairId
            };

            var response = await this.mediator.Send(query);
            return this.Ok(response);
        }

        [HttpPut]
        [Route("{repairId}")]
        public async Task<IActionResult> UpdateRepair([FromBody] UpdateRepairCommand command, int repairId)
        {
            command.RepairId = repairId;

            var response = await this.mediator.Send(command);
            return this.Ok(response);
        }

        [HttpDelete]
        [Route("{repairId}")]
        public async Task<IActionResult> DeleteRepair([FromRoute] int repairId)
        {
            var command = new DeleteRepairCommand()
            {
                RepairId = repairId
            };

            await this.mediator.Send(command);
            return NoContent();
        }
    }
}
