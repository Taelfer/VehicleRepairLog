using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRepairLog.ApplicationServices.API.Domain.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public int VinNumber { get; set; }
        public string PaintColor { get; set; }
        public string FuelType { get; set; }
        public int Mileage { get; set; }
    }
}
