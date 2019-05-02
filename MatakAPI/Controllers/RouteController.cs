using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MatakDBConnector;
using MatakAPI.Models;
//using BAMCIS.GeoJSON;
using Newtonsoft.Json;
using GeoJSON.Net.Feature;

namespace MatakAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : Controller
    {
        /*
        // POST: /api/Route/setGeoJSON
        [HttpGet("setGeoJSON")]
        [HttpPost]
        public IActionResult setGeoJSON([FromBody]string geojson)
        {
            return Ok(geojson);
        }

        */
        // POST: /api/Route/setRoute
        [HttpPost("SetRoute")]
        public IActionResult setRoute([FromBody] Route newRoute)
        {

            string errorString = null;
            SetRoute setter = new SetRoute();

            setter.AddNewRoute(newRoute,out errorString);

            return Ok(errorString);
        }


        // POST: /api/Route/GetAll
        [HttpGet("GetAll")]
        public IActionResult GetAllRoutes()
        {
            string errorString = null;
            GetRoute getter = new GetRoute();

            List<Route> obj = getter.AllRoutes(out errorString);
            return new JsonResult(obj);

        }

        // Get: /api/Route/GetRoute/5
        [HttpGet("GetRoute/{%d}")]    
        public IActionResult GetRoute(int id)
        {
            string errorString = null;
            GetRoute getter = new GetRoute();
            Route myRoute = getter.ById(id, out errorString);
            return new JsonResult(myRoute);
        }

        // POST: /api/Route/SetRoute
        [HttpGet("SetRoute")]
        public IActionResult SetRoute()
        {
            return Ok("You have Update the Route.");
        }
        [HttpPost]
        public IActionResult DeleteRoute()
        {
            return Ok("You have Deleted the Route.");
        } 
    }
}
