using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.Services;
using BL.BInterfaces;
using BL.Utils;
using BL.BModel;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LogController : Controller
    {
        ILogDetailService logDetail;

        public LogController(ILogDetailService service)
        {
            logDetail = service;
        }
        public ActionResult Index()
        {
            return View(AutoMapper<IEnumerable<BLogDetail>,List<LoggerModel>>.Map(logDetail.GetLogs));
        }

        public ActionResult Delete(int id)
        {
            logDetail.DeleteLog(id);
            return RedirectToAction("Index");
        }

    }
}
