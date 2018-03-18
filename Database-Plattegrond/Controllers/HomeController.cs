using Database_Plattegrond.DatabaseService;
using Database_Plattegrond.Models;
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
            TestDatabaseService a = new TestDatabaseService();
            //a.DoInsertQuery();
            //a.DoGetQuery();
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