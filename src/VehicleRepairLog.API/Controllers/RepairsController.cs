using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Features.Repairs;
using VehicleRepairLog.Application.Models;

namespace VehicleRepairLog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RepairsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddRepair([FromBody] AddRepairCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRepairs([FromQuery] GetAllRepairsQuery query)
        {
            List<RepairDto> response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{repairId}")]
        public async Task<IActionResult> GetRepairById([FromRoute] int repairId)
        {
            GetRepairByIdQuery query = new()
            {
                RepairId = repairId
            };

            RepairDto response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPut("{repairId}")]
        public async Task<IActionResult> UpdateRepair([FromBody] UpdateRepairCommand command, int repairId)
        {
            command.RepairId = repairId;

            RepairDto response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("{repairId}")]
        public async Task<IActionResult> DeleteRepair([FromRoute] int repairId)
        {
            DeleteRepairCommand command = new()
            {
                RepairId = repairId
            };

            await _mediator.Send(command);
            return NoContent();
        }
    }
}
