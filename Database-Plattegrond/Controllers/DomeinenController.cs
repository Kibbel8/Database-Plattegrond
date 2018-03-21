using Database_Plattegrond.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Database_Plattegrond.Controllers
{
    public class DomeinenController : Controller
    {
        // GET: Domeinen
        public ActionResult Index()
        {
            ViewBag.Message = "Domeinen";

            return View();
        }


        public ActionResult DomeinenBewerken()
        {
            Domein a = new Domein { Id = "HAHA", Naam = "Kunst", SubdomeinVan = "" };
            return View(a);
        }

        public ActionResult Toevoegen()
        {
            ViewBag.Message = "Domein toevoegen";

            return View();
        }
    }
}