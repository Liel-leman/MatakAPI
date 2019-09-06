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
using System.IO;

namespace MatakAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : Controller
    {

        [HttpPost("SetRoute")]
        public async Task<IActionResult> setRoute([ModelBinder(BinderType = typeof(JsonModelBinder))] Route newRoute, IList<IFormFile> files)
        {

            var routeModel = new RouteModel();
            var organizationModel = new OrganizationModel();
            int count = 0;
            string errorString = null;
            try
            {

                //route
                newRoute.OrgId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type.Equals("OrgId")).Value);
                newRoute.CreatedByUserId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type.Equals("UserId")).Value);
                count = routeModel.GetRoutesCountByOrgId(newRoute.OrgId, out errorString);
                newRoute.Name = organizationModel.getOrganizationById(newRoute.OrgId, out errorString).Name;
                newRoute.Name += " " + (count + 1);// name = OragnizationName+" number" ex: "UNDSS 32"

                newRoute.RouteId = routeModel.AddNewRoute(newRoute, out errorString);//route with the new params is added
                RouteObj obj = new RouteObj(newRoute);

                //before working with files we need to check if there is a directory
                var checkPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).ToString(), "Routes", newRoute.Name);

                if (!Directory.Exists(checkPath))
                {
                    Directory.CreateDirectory(checkPath);
                }

                foreach (var file in files)
                {
                    string fileName = file.FileName;
                    var filePath = Path.Combine(checkPath, fileName);

                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileSteam);
                    }
                }
                return new JsonResult(obj);
            }
            catch (Exception e)
            {
                return Ok(e + "\n" + errorString);
            }
        }
    



        /* [HttpPost("SetRoute")]
        public IActionResult setRoute([FromBody] Route newRoute)
        {
            int count = 0;
            string errorString = null;
            try
            {
                newRoute.OrgId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type.Equals("OrgId")).Value);
                newRoute.CreatedByUserId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type.Equals("UserId")).Value);
                RouteModel RouteModel = new RouteModel();
                OrganizationModel orgModel = new OrganizationModel();
                count = RouteModel.GetRoutesCountByOrgId(newRoute.OrgId, out errorString);
                newRoute.Name = orgModel.getOrganizationById(newRoute.OrgId, out errorString).Name;
                newRoute.Name += " " + (count + 1);
                
                newRoute.RouteId = RouteModel.AddNewRoute(newRoute, out errorString);
                RouteObj obj = new RouteObj(newRoute);
                return new JsonResult(obj);
            }
            catch (Exception e)
            {
                return Ok(e + "\n" + errorString);
            }

        }
        */

        [HttpPost("UpdateRoute")]
        public IActionResult UpdateRoute([FromBody] Route newRoute)
        {

            string errorString = null;
            try
            {
                RouteModel routeModel = new RouteModel();
                if (newRoute.RouteId == 0)
                    return BadRequest("please enter 'RouteID'");
                if(newRoute.StatusId== 4)//in case the route status is 'approved' , we update the approve user in route attribute
                    newRoute.ApprovedByUserId= Int32.Parse(User.Claims.FirstOrDefault(x => x.Type.Equals("UserId")).Value);
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
                RouteModel routeModel = new RouteModel();
                List<Route> obj = routeModel.GetAllRoutes(out errorString);
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
                RouteModel routeModel = new RouteModel();

                Route obj = routeModel.GetRouteById(id, out errorString);
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

        [HttpGet("GetReasons")]
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
                RouteModel routeModel = new RouteModel();
                List<Route> obj = routeModel.GetAllRoutesByOrgId(Orgid, out errorString);
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
                int MyID = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type.Equals("UserId")).Value);
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
