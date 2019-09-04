using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatakDBConnector;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MatakAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LandmarkController : Controller
    {
        [HttpPost("SetLandmark")]
        public IActionResult setLandmark([FromBody] Landmark newLandmark)
        {
            int count = 0;
            string errorString = null;
            try
            {
                LandmarkModel LandmarkModel = new LandmarkModel();
                OrganizationModel orgModel = new OrganizationModel();

                newLandmark.LandmarkId = LandmarkModel.AddNewLandmark(newLandmark, out errorString);

                return new JsonResult(LandmarkModel);
            }
            catch (Exception e)
            {
                return Ok(e + "\n" + errorString);
            }

        }

        [HttpPost("UpdateLandmark")]
        public IActionResult UpdateLandmark([FromBody] Landmark newLandmark)
        {

            string errorString = null;
            try
            {
                LandmarkModel landmarkModel = new LandmarkModel();
                if (newLandmark.LandmarkId == 0)
                    return BadRequest("please enter 'LandmarkID'");
                newLandmark.LandmarkId = landmarkModel.UpdateLandmarkId(newLandmark, out errorString);

                return new JsonResult(newLandmark);

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
                return Ok(e + "\n" + errorString);
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
    }
}