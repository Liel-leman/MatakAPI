using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatakAPI.Models;
using MatakDBConnector;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatakAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrgController : ControllerBase
    {
        // GET: api/Org/GetAll
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            string errorString = null;
            List<OrgObj> OrgObjects = new List<OrgObj>();
            OrganizationController orgCont = new OrganizationController();
            List<Organization> obj = orgCont.getAllOrganizations(out errorString);
            foreach (var org in obj)
            {
                OrgObjects.Add(new OrgObj(org));
            }
             return new JsonResult(OrgObjects);

        }
       
    }
}
