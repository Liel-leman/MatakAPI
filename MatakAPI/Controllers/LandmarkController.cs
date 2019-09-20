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
    public class LandmarkController : Controller
    {
        [HttpPost("SetLandmark")]
        public async Task<IActionResult> setLandmark([ModelBinder(BinderType = typeof(JsonModelBinder))] Landmark newLandmark, IList<IFormFile> files)
        {
            string errorString = null;
            try
            {
                newLandmark.CreatedByUserId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type.Equals("UserId")).Value);
                LandmarkModel LandmarkModel = new LandmarkModel();
                OrganizationModel orgModel = new OrganizationModel();
                newLandmark.LandmarkId = LandmarkModel.AddNewLandmark(newLandmark, out errorString);

                await new FileHelper().FilesAsync(newLandmark, files, true);
                return new JsonResult(newLandmark.LandmarkId);

            }
            catch (Exception e)
            {
                return BadRequest(e + "\n" + errorString);
            }

        }
        //TO DO --- update oleg on error
        [HttpPost("UpdateLandmark")]
        public IActionResult UpdateLandmark([FromBody] Landmark newLandmark)
        {

            string errorString = null;
            try
            {
                LandmarkModel landmarkModel = new LandmarkModel();
                if (newLandmark.LandmarkId == 0)
                    return BadRequest("please enter 'LandmarkID'");
                newLandmark.UpdatedByUserId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type.Equals("UserId")).Value);
                newLandmark.LandmarkId = landmarkModel.UpdateLandmarkId(newLandmark, out errorString);
                return new JsonResult(newLandmark);
            }
            catch (Exception e)
            {
                return BadRequest(e + "\n" + errorString);
            }

        }
        // Get: /api/Landmark/5
        [HttpGet("{id}")]
        public IActionResult GetLandmark(int id)
        {

            string errorString = null;
            try
            {
                LandmarkModel landmarkModel = new LandmarkModel();

                Landmark obj = landmarkModel.GetLandmarkById(id, out errorString);
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

        // POST: /api/Landmark/GetAll
        [HttpGet("GetAll")]
        public IActionResult GetAllLandmarks()
        {

            string errorString = null;
            try
            {
                LandmarkModel landmarkModel = new LandmarkModel();
                List<Landmark> obj = landmarkModel.GetAllLandmarks(out errorString);
                return new JsonResult(obj);
            }
            catch (Exception e)
            {
                return BadRequest(e + "\n\n Exception FROM DB \n" + errorString);
            }
        }


        [Route("GetLandmarksByCreatorID/{cratedUserId}")]
        [HttpGet]
        public IActionResult GetLandmarksByCreatorID(int cratedUserId)
        {

            string errorString = null;
            try
            {
                LandmarkModel LandmarkMethods = new LandmarkModel();
                List<Landmark> obj = LandmarkMethods.GetAllLandmarksByCreatorId(cratedUserId, out errorString);
                return new JsonResult(obj);
            }
            catch (Exception e)
            {
                return BadRequest(e + "\n" + errorString);
            }
        }

        [Route("GetAllDucomentsByID/{LandmarkdID}")]
        [HttpGet]
        public IActionResult GetAllDucomentsByID(int LandmarkdID)
        {
            string errorString = null;
            try
            {
                List<String> obj = new List<String>();
                List<Document> documents = new DocumentModel().GetAllDocumentsByRouteLanmdmarkId(LandmarkdID, true, out errorString);
                foreach (var document in documents)
                {
                    obj.Add(document.Filename);//TODO OLEG give the full file name
                }
                return new JsonResult(obj);


            }
            catch (Exception e)
            {
                return Ok(e + "\n" + errorString);
            }
        }



    }
}