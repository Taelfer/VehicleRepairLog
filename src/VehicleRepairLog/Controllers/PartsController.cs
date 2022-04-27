using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Features.Parts;

namespace VehicleRepairLog.Controllers
{
    [Authorize]
    [Route("api/{controller}")]
    [ApiController]
    public class PartsController : ControllerBase
    {
        private readonly IMediator mediator;

        public PartsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddPart([FromBody] AddPartCommand command)
        {
            await this.mediator.Send(command);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllParts([FromQuery] GetAllPartsQuery query)
        {
            var response = await this.mediator.Send(query);
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
            return this.Ok(response);
        }

        [HttpPut]
        [Route("{partId}")]
        public async Task<IActionResult> UpdatePart([FromBody] UpdatePartCommand command, int partId)
        {
            command.PartId = partId;

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

            await this.mediator.Send(command);
            return NoContent();
        }
    }
}
