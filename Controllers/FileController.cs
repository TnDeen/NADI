

using MVC5.Common;
using OfficeOpenXml;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace MVC5.Controllers
{
    public class FileController : BaseController
    { 
        //
        // GET: /File/
        public ActionResult Index(int id)
        {
            var fileToRetrieve = db.Files.Find(id);
            return File(fileToRetrieve.Content, fileToRetrieve.ContentType);
        }

        public FileResult Download(int id)
        {
            var fileToRetrieve = db.Files.Find(id);
            return File(fileToRetrieve.Content, fileToRetrieve.ContentType);
        }

        public FileResult DownloadFile()
        {
            string path = HttpContext.Server.MapPath("~/Template/Template/MasterTemplate.xlsx");
            return File(path, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public ActionResult Upload(FormCollection formCollection)
        {
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {

                    string fileName = file.FileName;
                    var basePath = Server.MapPath("~/App_Data/uploads");
                    var path = Path.Combine(basePath, fileName);
                    if (!Directory.Exists(basePath))
                    {
                        Directory.CreateDirectory(basePath);
                    }
                    file.SaveAs(path);

                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    

                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var workSheet = package.Workbook.Worksheets["BelianTemplate"];
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;

                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {
                           
                            var nameraw = workSheet.Cells[rowIterator, 1].Value;

                            if (nameraw == null)
                            {
                                break;
                            }
                            String nameVal = nameraw.ToString();
                            String rawdtval = workSheet.Cells[rowIterator, 4].Value.ToString();
                            String[] dtarray = rawdtval.Split('.');
                            String kgVal = workSheet.Cells[rowIterator, 2].Value.ToString();
                            String rmVal = workSheet.Cells[rowIterator, 3].Value.ToString();
                            String dtVal = dtarray[0] + "/" + dtarray[1] + "/" + dtarray[2];

                           
                           
                          
                        }
                    }

                }
            }
            return View();
        }
    }
}