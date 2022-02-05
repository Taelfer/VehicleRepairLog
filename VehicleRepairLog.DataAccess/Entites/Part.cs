using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRepairLog.DataAccess.Entites
{
    internal class Part : EntityBase
    {
        public string Name { get; set; }
        public string BrandName { get; set; }
        public decimal Price { get; set; }
        public Repair Repair { get; set; }
        public int RepairId { get; set; }
    }
}
