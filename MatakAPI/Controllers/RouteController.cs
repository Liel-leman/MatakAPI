using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MatakDBConnector;
using MatakAPI.Models;

namespace MatakAPI.Controllers
{
    [ApiController]
    public class RouteController : Controller
    {


        [HttpPost("TEMP")]
        public IActionResult PostTemp()
        {

            RouteObj minRoute = new RouteObj();
            minRoute.orgId = 0;
            return Ok("Temp") ;
        }


        [HttpPost("GetAllRoutes")]
        public IActionResult GetAllRoutes()
        {
            string errorString = null;
            GetRoute getter = new GetRoute();

            List<Route> obj = getter.AllRoutes(out errorString);
            return new JsonResult(obj);

        }
        [HttpGet("GetRoute/{id}")]
        public IActionResult GetRoute(int id)
        {
            string errorString = null;
            GetRoute getter = new GetRoute();
            Route myRoute = getter.ById(id, out errorString);
            return new JsonResult(myRoute);
        }

        [HttpPost("SetRoute")]
        public IActionResult SetRoute()
        {
            return Ok("You have Update the Route.");
        }
        [HttpPost("DeleteRoute")]
        public IActionResult DeleteRoute()
        {
            return Ok("You have Deleted the Route.");
        }

        [HttpGet("HomePage")]
        public ActionResult<IEnumerable<string>> HomePage()
        {
            return Ok(
                "** Welcome to matak APP ** "
                + System.Environment.NewLine +
                " all the methodes are [HttpPost]"
                + System.Environment.NewLine +
                " https://matakcloud.azurewebsites.net/GetAllRoutes - get all the routes  "
                 + System.Environment.NewLine +
                " https://matakcloud.azurewebsites.net/GetRoute/{id} - get spesific route "
            );
        }
        [HttpGet("")]
        public ActionResult<IEnumerable<string>> startPage()
        {
            return Ok(
                "** Welcome to matak APP ** "
                + System.Environment.NewLine +
                " all the methodes are [HttpPost]"
                + System.Environment.NewLine +
                " https://matakcloud.azurewebsites.net/GetAllRoutes - get all the routes  "
                 + System.Environment.NewLine +
                " https://matakcloud.azurewebsites.net/GetRoute/{id} - get spesific route "
            );
        }
    }
}
