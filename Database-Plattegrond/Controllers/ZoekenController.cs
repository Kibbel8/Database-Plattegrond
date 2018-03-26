using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Database_Plattegrond.Models;
using Database_Plattegrond.DatabaseService;
using System.Web.Routing;

namespace Database_Plattegrond.Controllers
{
    public class ZoekenController : Controller
    {
        // GET: Zoeken
        public ActionResult Index(string zoekterm = "")
        {
            ViewBag.Message = "Zoeken";

            ZoekenDatabaseService zds = new ZoekenDatabaseService();
            ZoekViewModel zvm = zds.GetAllSearchedDatasets(zoekterm);

            return View(zvm);
        }
    }
}