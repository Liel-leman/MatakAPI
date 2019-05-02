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
    public class UsrController : ControllerBase
    {
        // GET: api/Usr/GetAll
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            string errorString = null;
            List<UsrObj> UsrObjects = new List<UsrObj>();
            UserController UserCont = new UserController();
            List<User> obj = UserCont.getAllUsers(out errorString);
            foreach (var item in obj)
            {
                UsrObjects.Add(new UsrObj(item));
            }
            return new JsonResult(UsrObjects);

        }
        
    }
}
