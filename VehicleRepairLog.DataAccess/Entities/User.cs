using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRepairLog.DataAccess.Entities
{
    public class User : EntityBase 
    {
        public string Name { get; set; }
        public List<Vehicle> Vehicles { get; set; }
    }
}
