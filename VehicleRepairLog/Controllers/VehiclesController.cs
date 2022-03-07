using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Vehicles;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Vehicles;

namespace VehicleRepairLog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VehiclesController : ApiControllerBase
    {
        private readonly IMediator mediator;

        public VehiclesController(IMediator mediator) : base(mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("")]
        public Task<IActionResult> AddVehicle([FromBody] AddVehicleRequest request)
        {
            return this.HandleRequest<AddVehicleRequest, AddVehicleResponse>(request);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllVehicles([FromQuery] GetAllVehiclesRequest request)
        {
            var response = await this.mediator.Send(request);
            return this.Ok(response);
        }

        [HttpGet]
        [Route("{vehicleId}")]
        public async Task<IActionResult> GetVehicleById([FromRoute] int vehicleId)
        {
            var request = new GetVehicleByIdRequest()
            {
                VehicleId = vehicleId
            };

            var response = await this.mediator.Send(request);
            return this.Ok(response);
        }

        [HttpPut]
        [Route("{vehicleId}")]
        public Task<IActionResult> UpdateVehicle([FromBody] UpdateVehicleRequest request, int vehicleId)
        {
            request.VehicleId = vehicleId;

            return this.HandleRequest<UpdateVehicleRequest, UpdateVehicleResponse>(request);
        }

        [HttpDelete]
        [Route("{vehicleId}")]
        public async Task<IActionResult> DeleteVehicle([FromRoute] int vehicleId)
        {
            var request = new DeleteVehicleRequest()
            {
                VehicleId = vehicleId
            };

            var response = await this.mediator.Send(request);
            return this.Ok(response);
        }
    }
}
