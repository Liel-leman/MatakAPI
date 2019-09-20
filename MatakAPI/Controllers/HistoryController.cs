using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatakDBConnector;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatakAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class HistoryController : Controller
    {

        // Get: /api/History/GetHistoryByRouteID/5
        [Route("GetHistoryByRouteID/{routeId}")]
        [HttpGet]
        public IActionResult GetHistory(int routeId)
        {

            string errorString = null;
            try
            {
                RouteHistoryModel historyModel = new RouteHistoryModel();

                List<RouteHistory> obj = historyModel.GetRouteHistoryListByRouteId(routeId, out errorString);
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
        // Get: /api/History/GetHistoryByOrgId/5
        [Route("GetHistoryByOrgId/{orgid}")]
        [HttpGet]
        public IActionResult GetHistorybyorgid(int orgid)
        {

            string errorString = null;
            try
            {
                RouteHistoryModel historyModel = new RouteHistoryModel();

                List<RouteHistory> obj = historyModel.GetRouteHistoryListByOrgId(orgid, out errorString);
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



        // POST: /api/History/GetAll
        [HttpGet("GetAll")]
        public IActionResult GetAllHistorys()
        {

            string errorString = null;
            try
            {
                RouteHistoryModel historyModel = new RouteHistoryModel();

                List<RouteHistory> obj = historyModel.GetAllRouteHistories(out errorString);
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