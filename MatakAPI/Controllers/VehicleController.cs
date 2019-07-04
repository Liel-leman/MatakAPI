using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatakAPI.Models;
using MatakDBConnector;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatakAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        // GET: api/Veh/GetAll
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            string errorString = null;
            try
            {
                List<VehObj> vehObjects = new List<VehObj>();
                VehicleModel vehicleModel = new VehicleModel();
                List<Vehicle> obj = vehicleModel.getAllVehicles(out errorString);
                foreach (var item in obj)
                {
                    vehObjects.Add(new VehObj(item));
                }
                return new JsonResult(vehObjects);
            }
            catch (Exception e)
            {
                return Ok(e + "\n" + errorString);
            }

        }
    }
}
