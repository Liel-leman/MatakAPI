using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatakAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : Controller
    {
        [HttpPost("upload")]
        public async Task<IActionResult> PostProfilePicture(IFormFile file)
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
        }
    }
}
