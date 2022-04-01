using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Features.Vehicles;

namespace VehicleRepairLog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IMediator mediator;

        public VehiclesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddVehicle([FromBody] AddVehicleCommand command)
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
        public async Task<IActionResult> GetAllVehicles([FromQuery] GetAllVehiclesQuery query)
        {
            var response = await this.mediator.Send(query);

            if (response is null)
            {
                return NotFound();
            }

            return this.Ok(response);
        }

        [HttpGet]
        [Route("{vehicleId}")]
        public async Task<IActionResult> GetVehicleById([FromRoute] int vehicleId)
        {
            var query = new GetVehicleByIdQuery()
            {
                VehicleId = vehicleId
            };

            var response = await this.mediator.Send(query);

            if (response is null)
            {
                return NotFound();
            }

            return this.Ok(response);
        }

        [HttpPut]
        [Route("{vehicleId}")]
        public async Task<IActionResult> UpdateVehicle([FromBody] UpdateVehicleCommand command, int vehicleId)
        {
            command.VehicleId = vehicleId;

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
        [Route("{vehicleId}")]
        public async Task<IActionResult> DeleteVehicle([FromRoute] int vehicleId)
        {
            var command = new DeleteVehicleCommand()
            {
                VehicleId = vehicleId
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
