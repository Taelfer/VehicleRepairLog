using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Features.Parts;
using VehicleRepairLog.Application.Models;

namespace VehicleRepairLog.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PartsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PartsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddPart([FromBody] AddPartCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllParts([FromQuery] GetAllPartsQuery query)
        {
            List<PartDto> response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{partId}")]
        public async Task<IActionResult> GetPartById([FromRoute] int partId)
        {
            GetPartByIdQuery query = new()
            {
                PartId = partId
            };

            PartDto response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPut("{partId}")]
        public async Task<IActionResult> UpdatePart([FromBody] UpdatePartCommand command, int partId)
        {
            command.PartId = partId;

            PartDto response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("{partId}")]
        public async Task<IActionResult> DeletePart([FromRoute] int partId)
        {
            DeletePartCommand command = new()
            {
                PartId = partId
            };

            await _mediator.Send(command);
            return NoContent();
        }
    }
}
