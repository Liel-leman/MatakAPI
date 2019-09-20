using MatakDBConnector;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MatakAPI.Models
{
    public class FileHelper
    {
        //private String directoryString = @"C:\Users\ליאל\source\repos\MatakAPI\MatakAPI\Files";//hard coded local
        private String directoryString = @"F:\GIS\Matak\Files";// hard codded server
        public async Task FilesAsync(Route newRoute,IList<IFormFile> files,Boolean isLandmark)
        {
            String errorString = null;
            foreach (var file in files)
            {
                Document doc = new Document(newRoute.RouteId, file.FileName, DateTime.UtcNow.Date, DateTime.UtcNow.Date, newRoute.CreatedByUserId, 0, isLandmark);
                int ducID = new DocumentModel().AddNewDocument(doc, out errorString);
                string fileName = new DocumentModel().GetDocumentHandleByDocId(ducID, out errorString);

                var filePath = Path.Combine(directoryString, fileName);
                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    
                    await file.CopyToAsync(fileSteam);
                }
            }
        }

        internal async Task FilesAsync(Landmark newLandmark, IList<IFormFile> files, Boolean isLandmark)
        {
            String errorString = null;
            foreach (var file in files)
            {
                Document doc = new Document(newLandmark.LandmarkId, file.FileName, DateTime.UtcNow.Date, DateTime.UtcNow.Date, newLandmark.CreatedByUserId, 0, isLandmark);
                int ducID = new DocumentModel().AddNewDocument(doc, out errorString);
                string fileName = new DocumentModel().GetDocumentHandleByDocId(ducID, out errorString);

                var filePath = Path.Combine(directoryString, fileName);
                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileSteam);
                }
            }
        }
    }
}
