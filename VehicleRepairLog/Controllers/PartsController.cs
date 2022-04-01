using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Features.Parts;

namespace VehicleRepairLog.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class PartsController : ControllerBase
    {
        private readonly IMediator mediator;

        public PartsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddPart([FromBody] AddPartCommand command)
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
        public async Task<IActionResult> GetAllParts([FromQuery] GetAllPartsQuery query)
        {
            var response = await this.mediator.Send(query);

            if (response is null)
            {
                return Unauthorized();
            }

            return this.Ok(response);
        }

        [HttpGet]
        [Route("{partId}")]
        public async Task<IActionResult> GetPartById([FromRoute] int partId)
        {
            var query = new GetPartByIdQuery()
            {
                PartId = partId
            };

            var response = await this.mediator.Send(query);

            if (response is null)
            {
                return NotFound();
            }

            return this.Ok(response);
        }

        [HttpPut]
        [Route("{partId}")]
        public async Task<IActionResult> UpdatePart([FromBody] UpdatePartCommand command, int partId)
        {
            command.PartId = partId;

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
        [Route("{partId}")]
        public async Task<IActionResult> DeletePart([FromRoute] int partId)
        {
            var command = new DeletePartCommand()
            {
                PartId = partId
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
