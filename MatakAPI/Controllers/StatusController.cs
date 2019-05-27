using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MatakDBConnector;

namespace MatakAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        // GET: api/Stat/GetAll
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            string errorString = null;
            StatusModel StatusCont = new StatusModel();
            List<Status> obj = StatusCont.getAllStati(out errorString);
            return new JsonResult(obj);

        }

    }
}
