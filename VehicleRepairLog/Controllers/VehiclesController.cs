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
        public VehiclesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Route("")]
        public Task<IActionResult> AddVehicle([FromBody] AddVehicleRequest request)
        {
            return this.HandleRequest<AddVehicleRequest, AddVehicleResponse>(request);
        }

        [HttpGet]
        [Route("")]
        public Task<IActionResult> GetAllVehicles([FromQuery] GetAllVehiclesRequest request)
        {
            return this.HandleRequest<GetAllVehiclesRequest, GetAllVehiclesResponse>(request);
        }

        [HttpGet]
        [Route("{vehicleId}")]
        public Task<IActionResult> GetVehicleById([FromRoute] int vehicleId)
        {
            var request = new GetVehicleByIdRequest()
            {
                VehicleId = vehicleId
            };

            return this.HandleRequest<GetVehicleByIdRequest, GetVehicleByIdResponse>(request);
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
        public Task<IActionResult> DeleteVehicle([FromRoute] int vehicleId)
        {
            var request = new DeleteVehicleRequest()
            {
                VehicleId = vehicleId
            };

            return this.HandleRequest<DeleteVehicleRequest, DeleteVehicleResponse>(request);
        }
    }
}
