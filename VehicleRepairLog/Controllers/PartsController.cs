using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Parts;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Parts;

namespace VehicleRepairLog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PartsController : ApiControllerBase
    {
        private readonly IMediator mediator;

        public PartsController(IMediator mediator) : base(mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("")]
        public Task<IActionResult> AddPart([FromBody] AddPartRequest request)
        {
            return this.HandleRequest<AddPartRequest, AddPartResponse>(request);
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
        public Task<IActionResult> GetPartById([FromRoute] int partId)
        {
            var request = new GetPartByIdRequest()
            {
                PartId = partId
            };

            return this.HandleRequest<GetPartByIdRequest, GetPartByIdResponse>(request);
        }
        
        [HttpPut]
        [Route("{partId}")]
        public async Task<IActionResult> UpdatePart([FromBody] UpdatePartRequest request, int partId)
        {
            request.PartId = partId;

            var response = await this.mediator.Send(request);
            return this.Ok(response);
        }

        [HttpDelete]
        [Route("{partId}")]
        public async Task<IActionResult> DeletePart([FromRoute] int partId)
        {
            var request = new DeletePartRequest()
            {
                PartId = partId
            };

            var response = await this.mediator.Send(request);
            return this.Ok(response);
        }
    }
}
