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
    public class UserController : ControllerBase
    {
        // GET: api/User/GetAll
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            string errorString = null;
            try
            {
                List<UsrObj> UsrObjects = new List<UsrObj>();
                UserModel userModel = new UserModel();
                List<User> obj = userModel.getAllUsers(out errorString);
                foreach (var item in obj)
                {
                    UsrObjects.Add(new UsrObj(item));
                }
                return new JsonResult(UsrObjects);
            }
            catch (Exception e)
            {
                return Ok(e + "\n" + errorString);
            }

        }
        [HttpGet("GetAllFull")]
        public IActionResult GetAllFull()
        {
            string errorString = null;
            try
            {
                UserModel userModel = new UserModel();
                List<User> obj = userModel.getAllUsers(out errorString);

                return new JsonResult(obj);
            }
            catch (Exception e)
            {
                return Ok(e + "\n" + errorString);
            }

        }

        [HttpGet("GetCurrentUser")]
        public IActionResult GetCurrentUser()
        {
            string errorString = null;
            try
            {
                int id = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type.Equals("UserId")).Value);
                User usr = new User();
                UserModel userModel = new UserModel();
                List<User> obj = userModel.getAllUsers(out errorString);
                foreach (var item in obj)
                {
                    if (item.UserId == id)
                        usr = item;
                }
                return new JsonResult(usr);
            }
            catch (Exception e)
            {
                return Ok(e + "\n" + errorString);
            }

        }


    }
}
