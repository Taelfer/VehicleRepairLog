using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VehicleRepairLog.DataAccess;
using VehicleRepairLog.DataAccess.Entites;

namespace VehicleRepairLog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly VehicleProfileStorageContext context;
        private readonly IRepository<Vehicle> vehicleRepository;

        public VehiclesController(VehicleProfileStorageContext context, IRepository<Vehicle> vehicleRepository)
        {
            this.context = context;
            this.vehicleRepository = vehicleRepository;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<Vehicle> GetAllVehicles() => this.vehicleRepository.GetAll();

        [HttpGet]
        [Route("{vehicleId}")]
        public Vehicle GetVehicleById([FromQuery]int vehicleId) => this.vehicleRepository.GetById(vehicleId);
    }
}
