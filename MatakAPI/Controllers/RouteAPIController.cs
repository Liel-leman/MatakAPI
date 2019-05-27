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
    [Route("api/Route")]
    [ApiController]
    public class RouteAPIController : Controller
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
            int count = 0;
            RouteController RouteCont = new RouteController();
            OrganizationController orgCont = new OrganizationController();
            count = RouteCont.GetRoutesCountByOrgId(newRoute.OrgId,out errorString);
            newRoute.Name = orgCont.getOrganizationById(newRoute.OrgId, out errorString).Name;
            newRoute.Name += " "+count+1;
            RouteCont = new RouteController();
            newRoute.RouteId = RouteCont.AddNewRoute(newRoute, out errorString);
            RouteObj obj = new RouteObj(newRoute);
            return new JsonResult(obj);


        }


        // POST: /api/Route/GetAll
        [HttpGet("GetAll")]
        public IActionResult GetAllRoutes()
        {
           
            string errorString = null;
            RouteController RouteCont = new RouteController();
            List<Route> obj = RouteCont.GetAllRoutes(out errorString);
            return new JsonResult(obj);



        }

        // Get: /api/Route/5
        [HttpGet("{id}")]    
        public IActionResult GetRoute(int id)
        {
           
            string errorString = null;
            RouteController RouteCont = new RouteController();
            
            Route obj = RouteCont.GetRouteById(id, out errorString);
            if (obj != null && errorString == null)
            {
                return new JsonResult(obj);
            }
            else
            {
                return BadRequest("cant find");
            }

        }

        [HttpPost("GetReasons")]
        public IActionResult GetReasons()
        {
            string errorString = null;
            ReasonController reasonController = new ReasonController();
            List<Reason> obj = reasonController.GetAllReasons(out errorString);
            return new JsonResult(obj);
        }

    }
}
