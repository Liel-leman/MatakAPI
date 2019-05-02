using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatakDBConnector;
namespace MatakAPI.Models
{
    public class VehObj
    {
        public int VehicleId { get; set; }
        public string PlateNumber { get; set; }
        
        public VehObj(Vehicle vehicle)
        {
            VehicleId = vehicle.VehicleId;
            PlateNumber = vehicle.PlateNumber;
        }
    }
}
