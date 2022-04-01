using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Features.Repairs;

namespace VehicleRepairLog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RepairsController : ControllerBase
    {
        private readonly IMediator mediator;

        public RepairsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddRepair([FromBody] AddRepairCommand command)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(
                    this.ModelState
                    .Where(x => x.Value.Errors.Any())
                    .Select(x => new { property = x.Key, errors = x.Value.Errors }));
            }

            var response = await this.mediator.Send(command);
            return NoContent();
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllRepairs([FromQuery] GetAllRepairsQuery query)
        {
            var response = await this.mediator.Send(query);

            if (response is null)
            {
                return NotFound();
            }

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

            if (response is null)
            {
                return NotFound();
            }

            return this.Ok(response);
        }

        [HttpPut]
        [Route("{repairId}")]
        public async Task<IActionResult> UpdateRepair([FromBody] UpdateRepairCommand command, int repairId)
        {
            command.RepairId = repairId;

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(
                    this.ModelState
                    .Where(x => x.Value.Errors.Any())
                    .Select(x => new { property = x.Key, errors = x.Value.Errors }));
            }

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

            var response = await this.mediator.Send(command);

            if (response is null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
