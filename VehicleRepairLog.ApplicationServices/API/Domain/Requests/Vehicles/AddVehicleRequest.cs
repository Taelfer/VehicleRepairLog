using MediatR;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Vehicles;

namespace VehicleRepairLog.ApplicationServices.API.Domain.Requests.Vehicles
{
    public class AddVehicleRequest : IRequest<AddVehicleResponse>
    {
        public string BrandName { get; set; }
        public int VinNumber { get; set; }
        public string PaintColor { get; set; }
        public string FuelType { get; set; }
        public int Mileage { get; set; }
        public int UserId { get; set; }
    }
}
