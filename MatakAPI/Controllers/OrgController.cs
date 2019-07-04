using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatakAPI.Models;
using MatakDBConnector;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatakAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrgController : ControllerBase
    {
        // GET: api/Org/GetAll
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
           // return Ok(User.Claims.FirstOrDefault(x => x.Type.Equals("orgId")).Value);

            string errorString = null;
            try
            {
                List<OrgObj> OrgObjects = new List<OrgObj>();
                OrganizationModel orgCont = new OrganizationModel();
                List<Organization> obj = orgCont.getAllOrganizations(out errorString);
                foreach (var org in obj)
                {
                    OrgObjects.Add(new OrgObj(org));
                }
                return new JsonResult(OrgObjects);
            }
            catch (Exception e)
            {
                return Ok(e + "\n" + errorString);
            }

        }
       
    }
}
