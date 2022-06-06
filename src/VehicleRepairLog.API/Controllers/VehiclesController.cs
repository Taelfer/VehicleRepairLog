using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Features.Vehicles;
using VehicleRepairLog.Application.Models;

namespace VehicleRepairLog.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VehiclesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicle([FromBody] AddVehicleCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVehicles([FromQuery] GetAllVehiclesQuery query, CancellationToken cancellationToken)
        {
            List<VehicleDto> response = await _mediator.Send(query, cancellationToken);
            return Ok(response);
        }

        [HttpGet("{vehicleId}")]
        public async Task<IActionResult> GetVehicleById([FromRoute] int vehicleId)
        {
            GetVehicleByIdQuery query = new()
            {
                VehicleId = vehicleId
            };

            VehicleDto response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPut("{vehicleId}")]
        public async Task<IActionResult> UpdateVehicle([FromBody] UpdateVehicleCommand command, int vehicleId)
        {
            command.VehicleId = vehicleId;

            VehicleDto response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("{vehicleId}")]
        public async Task<IActionResult> DeleteVehicle([FromRoute] int vehicleId)
        {
            DeleteVehicleCommand command = new()
            {
                VehicleId = vehicleId
            };

            await _mediator.Send(command);
            return NoContent();
        }
    }
}
