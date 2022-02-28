﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Repairs;

namespace VehicleRepairLog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RepairsController : ControllerBase
    {
        private readonly IMediator mediator;

        public RepairsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddRepair([FromBody] AddRepairRequest request)
        {
            var response = await this.mediator.Send(request);
            return this.Ok(response);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllRepairs([FromQuery] GetAllRepairsRequest request)
        {
            var response = await this.mediator.Send(request);
            return Ok(response);
        }
    }
}
