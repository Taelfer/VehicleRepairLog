using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Parts;

namespace VehicleRepairLog.Controllers
{
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
        public async Task<IActionResult> AddPart([FromBody] AddPartRequest request)
        {
            var response = await this.mediator.Send(request);
            return this.Ok(response);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllParts([FromQuery] GetAllPartsRequest request)
        {
            var response = await this.mediator.Send(request);
            return this.Ok(response);
        }

        [HttpGet]
        [Route("{partId}")]
        public async Task<IActionResult> GetPartById([FromRoute] int partId)
        {
            var request = new GetPartByIdRequest()
            {
                PartId = partId
            };

            var response = await this.mediator.Send(request);
            return this.Ok(response);
        }
        
        [HttpPut]
        [Route("partId")]
        public async Task<IActionResult> UpdatePart([FromBody] UpdatePartRequest request, int partId)
        {
            request.PartId = partId;

            var response = await this.mediator.Send(request);
            return this.Ok(response);
        }
    }
}
