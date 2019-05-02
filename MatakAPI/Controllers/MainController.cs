using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatakAPI.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {
        // Get: nothing to type
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
