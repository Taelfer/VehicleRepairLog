using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRepairLog.ApplicationServices.API.Domain.Models
{
    public class Repair
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string CarWorkshopName { get; set; }
    }
}
