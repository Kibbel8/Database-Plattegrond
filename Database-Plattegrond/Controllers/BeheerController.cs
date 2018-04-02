using Database_Plattegrond.DatabaseService;
using Database_Plattegrond.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Database_Plattegrond.Controllers
{
    public class BeheerController : Controller
    {
        // GET: Beheerder
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Domeinen()
        {
            ViewBag.Message = "Domeinen";

            DomeinenDatabaseService DDS = new DomeinenDatabaseService();
            DomeinenViewModel DVM = new DomeinenViewModel { Domeinen = DDS.GetHoofdDomeinen() };

            return View(DVM);
        }

    }
}