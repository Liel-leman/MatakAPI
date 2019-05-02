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
                "** Welcome to matak API ** "
                 + System.Environment.NewLine +
                " https://documenter.getpostman.com/view/7173606/S1LsXpsP - Rest API for Matak Project fully described "
            );
        }
    }
}
