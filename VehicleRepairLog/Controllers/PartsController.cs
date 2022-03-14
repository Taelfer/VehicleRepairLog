using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Parts;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Parts;

namespace VehicleRepairLog.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class PartsController : ApiControllerBase
    {
        public PartsController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        [Route("")]
        public Task<IActionResult> AddPart([FromBody] AddPartRequest request)
        {
            return this.HandleRequest<AddPartRequest, AddPartResponse>(request);
        }

        [HttpGet]
        [Route("")]
        public Task<IActionResult> GetAllParts([FromQuery] GetAllPartsRequest request)
        {
            return this.HandleRequest<GetAllPartsRequest, GetAllPartsResponse>(request);
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
        public Task<IActionResult> UpdatePart([FromBody] UpdatePartRequest request, int partId)
        {
            request.PartId = partId;

            return this.HandleRequest<UpdatePartRequest, UpdatePartResponse>(request);
        }

        [HttpDelete]
        [Route("{partId}")]
        public Task<IActionResult> DeletePart([FromRoute] int partId)
        {
            var request = new DeletePartRequest()
            {
                PartId = partId
            };

            return this.HandleRequest<DeletePartRequest, DeletePartResponse>(request);
        }
    }
}
