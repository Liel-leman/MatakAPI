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
using Microsoft.AspNetCore.Authorization;

namespace MatakAPI.Controllers
{
    [Authorize]
    [Route("api/Route")]
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
            try
            {
                int count = 0;
                RouteModel RouteModel = new RouteModel();
                OrganizationModel orgModel = new OrganizationModel();
                count = RouteModel.GetRoutesCountByOrgId(newRoute.OrgId, out errorString);
                newRoute.Name = orgModel.getOrganizationById(newRoute.OrgId, out errorString).Name;
                newRoute.Name += " " + (count + 1);
                newRoute.CreatedByUserId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type.Equals("UsedId")).Value);
                newRoute.RouteId = RouteModel.AddNewRoute(newRoute, out errorString);
                RouteObj obj = new RouteObj(newRoute);
                return new JsonResult(obj);
            }
            catch (Exception e)
            {
                return Ok(e + "\n" + errorString);
            }

        }

        [HttpPost("UpdateRoute")]
        public IActionResult UpdateRoute([FromBody] Route newRoute)
        {

            string errorString = null;
            try
            {
                RouteModel routeModel = new RouteModel();
                if (newRoute.RouteId == 0)
                    return BadRequest("please enter ID");
                newRoute.RouteId = routeModel.UpdateRouteId(newRoute, out errorString);
                RouteObj obj = new RouteObj(newRoute);
                return new JsonResult(obj);

            }
            catch (Exception e)
            {
                return Ok(e + "\n" + errorString);
            }

        }


        // POST: /api/Route/GetAll
        [HttpGet("GetAll")]
        public IActionResult GetAllRoutes()
        {
           
            string errorString = null;
            try
            {
                RouteModel RouteCont = new RouteModel();
                List<Route> obj = RouteCont.GetAllRoutes(out errorString);
                return new JsonResult(obj);
            }
            catch (Exception e)
            {
                return Ok(e + "\n" + errorString);
            }



        }

        // Get: /api/Route/5
        [HttpGet("{id}")]    
        public IActionResult GetRoute(int id)
        {
           
            string errorString = null;
            try
            {
                RouteModel RouteModel = new RouteModel();

                Route obj = RouteModel.GetRouteById(id, out errorString);
                if (obj != null && errorString == null)
                {
                    return new JsonResult(obj);
                }
                else
                {
                    return BadRequest("cant find");
                }
            }
            catch (Exception e)
            {
                return Ok(e + "\n" + errorString);
            }
        }

        [HttpPost("GetReasons")]
        public IActionResult GetReasons()
        {
            string errorString = null;
            try
            {
                ReasonModel reasonModel = new ReasonModel();
                List<Reason> obj = reasonModel.GetAllReasons(out errorString);
                return new JsonResult(obj);
            }
            catch (Exception e)
            {
                return Ok(e + "\n" + errorString);
            }
        }

        [Route("GetRouteByOrgID/{Orgid}")]
        [HttpGet]
        public IActionResult GetRouteByOrgID(int Orgid)
        {
            string errorString = null;
            try
            {
                RouteModel RouteMethods = new RouteModel();
                List<Route> obj = RouteMethods.GetAllRoutesByOrgId(Orgid, out errorString);
                return new JsonResult(obj);
            }
            catch(Exception e)
            {
                return Ok(e + "\n" + errorString);
            }
        }

        [Route("GetRouteByCreatorID/{userid}")]
        [HttpGet]
        public IActionResult GetRouteByCreatorID(int userid)
        {
            string errorString = null;
            try
            {
                RouteModel RouteMethods = new RouteModel();
                List<Route> obj = RouteMethods.GetAllRoutesByCreatorId(userid, out errorString);
                return new JsonResult(obj);
            }
            catch (Exception e)
            {
                return Ok(e + "\n" + errorString);
            }
        }

        [Route("GetRoutByReceiverId")]
        [HttpGet]
        public IActionResult GetRoutByReceiverId()
        {
            
            string errorString = null;
            try
            {
                int MyID = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type.Equals("UsedId")).Value);
                RouteModel RouteMethods = new RouteModel();
                List<Route> obj = RouteMethods.GetAllRoutesByReceiverId(MyID, out errorString);
                return new JsonResult(obj);
            }
            catch (Exception e)
            {
                return Ok(e + "\n" + errorString);
            }
        }

    }
}
