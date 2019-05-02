using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatakAPI.Models;
using MatakDBConnector;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatakAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehController : ControllerBase
    {
        // GET: api/Veh/GetAll
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            string errorString = null;
            List<VehObj> vehObjects = new List<VehObj>();
            VehicleController vehicleCont = new VehicleController();
            List<Vehicle> obj = vehicleCont.getAllVehicles(out errorString);
            foreach (var item in obj)
            {
                vehObjects.Add(new VehObj(item));
            }
            return new JsonResult(vehObjects);

        }
    }
}
