using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRepairLog.DataAccess.Entites
{
    internal class Vehicle : EntityBase 
    {
        public string BrandName { get; set; }
        public int VinNumber { get; set; }
        public string PaintColor { get; set; }
        public string FuelType { get; set; }
        public int Mileage { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public List<Repair> Repairs { get; set; }
    }
}
