using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Database_Plattegrond.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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

        public ActionResult Dataset_Kunst()
        {
            ViewBag.Message = "Kunst dataset";

            return View();
        }

        public ActionResult Domeinen()
        {
            ViewBag.Message = "Domeinen";

            return View();
        }
    }
}