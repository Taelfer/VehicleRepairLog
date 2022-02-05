using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRepairLog.DataAccess.Entites
{
    internal class Repair : EntityBase
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string CarWorkshopName { get; set; }
        public Vehicle Vehicle { get; set; }
        public int VehicleId { get; set; }
        public List<Part> Parts { get; set; }
    }
}
