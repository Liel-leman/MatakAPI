using System;
using System.Collections.Generic;
using System.IO;
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
    public class FilesController : Controller
    {
        [HttpPost("Uploadv2")]
        public async Task<IActionResult> UploadV2([ModelBinder(BinderType = typeof(JsonModelBinder))] Route newRoute, IList<IFormFile> files)
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
                newRoute.Name =  organizationModel.getOrganizationById(newRoute.OrgId, out errorString).Name;
                newRoute.Name += " " + (count + 1);// name = OragnizationName+" number" ex: "UNDSS 32"

                newRoute.RouteId = routeModel.AddNewRoute(newRoute, out errorString);//route with the new params is added
                RouteObj obj = new RouteObj(newRoute);

                //before working with files we need to check if there is a directory
                var checkPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).ToString(),"Routes", newRoute.Name);
     
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



        /*
        [HttpPost("upload")]
        public async Task<IActionResult> upload(IFormFile file)
        {
            try
            {
                string fileName = file.FileName;
                System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString();
                var checkPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).ToString(), "FilesUploadedTest");

                if (!Directory.Exists(checkPath))
                {
                    Directory.CreateDirectory(checkPath);
                }

                var filePath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).ToString(), "FilesUploadedTest", fileName);
                
                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileSteam);
                }
                return Ok("working!");
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        } */
    }
   
}
