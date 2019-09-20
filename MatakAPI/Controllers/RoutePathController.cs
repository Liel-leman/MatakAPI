using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatakDBConnector;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MatakAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoutePathController : Controller
    {
        // POST: /api/RoutePath/GetAll
        [HttpGet("GetAll")]
        public IActionResult GetAllRoutePaths()
        {
            string errorString = null;
            try
            {
                RoutePathModel routePathModel = new RoutePathModel();
                List<RoutePath> obj = routePathModel.GetAllRoutePaths(out errorString);
                return new JsonResult(obj);
            }
            catch (Exception e)
            {
                return Ok(e + "\n" + errorString);
            }
        }


        // POST: /api/RoutePath/SetRoutePath
        [HttpPost("SetRoutePath")]
        public IActionResult setRoutePath([FromBody] RoutePath newRoutePath)
        {
            string errorString = null;
            try
            {
                newRoutePath.CreatedByUserId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type.Equals("UserId")).Value);
                RoutePathModel RoutePathModel = new RoutePathModel();
                newRoutePath.RoutePathId = RoutePathModel.AddNewRoutePath(newRoutePath, out errorString);
                return new JsonResult(newRoutePath.RoutePathId);
            }
            catch (Exception e)
            {
                return Ok(e + "\n" + errorString);
            }
        }
        // POST: /api/RoutePath/UpdateRoutePath
        [HttpPost("UpdateRoutePath")]
        public IActionResult UpdateRoutePath([FromBody] RoutePath newRoutePath)
        {

            string errorString = null;
            try
            {
                newRoutePath.UpdatedByUserId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type.Equals("UserId")).Value);
                RoutePathModel routePathModel = new RoutePathModel();
                newRoutePath.Updated = DateTime.Now;
                newRoutePath.RoutePathId = routePathModel.UpdateRoutePathByRoutePathId(newRoutePath, out errorString);
                return new JsonResult(newRoutePath);
            }
            catch (Exception e)
            {
                return Ok(e + "\n" + errorString);
            }

        }

        // Get: /api/RoutePath/5
        [HttpGet("{routePathId}")]
        public IActionResult GetRoutePathByRoutePathID(int routePathId)
        {

            string errorString = null;
            try
            {
                RoutePathModel routePathModel = new RoutePathModel();
                RoutePath obj = routePathModel.GetRoutePathByRoutePathID(routePathId, out errorString);
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


        [Route("GetRoutePathByCreatorID/{userid}")]
        [HttpGet]
        public IActionResult GetRoutePathByCreatorID(int userid)
        {

            string errorString = null;
            try
            {
                RoutePathModel RoutePathMethods = new RoutePathModel();
                List<RoutePath> obj = RoutePathMethods.RoutePathByCreatedByUser(userid, out errorString);
                return new JsonResult(obj);
            }
            catch (Exception e)
            {
                return Ok(e + "\n" + errorString);
            }
        }

   





    }
}