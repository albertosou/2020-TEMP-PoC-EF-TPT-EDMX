using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoC_EF_TPT.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var ctx = Data.Entities.GetEntities())
            {
                if (!ctx.Operation.OfType<Data.OperationImport>().Any())
                {
                    ctx.Operation.Add(new Data.OperationImport()
                    {
                        Id = Guid.NewGuid(),
                        Active = true,
                        CreateDate = DateTime.Now,
                        ImportBasePath = "%IMPORT_PATH%"
                    });
                    ctx.SaveChanges();
                }
                if (!ctx.Operation.OfType<Data.OperationExport>().Any())
                {
                    ctx.Operation.Add(new Data.OperationExport()
                    {
                        Id = Guid.NewGuid(),
                        Active = true,
                        CreateDate = DateTime.Now,
                        ExportBasePath = "%EXPORT_PATH%",
                        OperationImport = ctx.Operation.OfType<Data.OperationImport>().ToList()
                    });
                    ctx.SaveChanges();
                }
                if (!ctx.OperationFile.Any())
                {
                    ctx.OperationFile.Add(new Data.OperationFile
                    {
                        Id = Guid.NewGuid(),
                        FilePath = "%FILE_PATH%",
                        Operation = ctx.Operation.FirstOrDefault()
                    });
                    ctx.SaveChanges();
                }

            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}