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

        public ActionResult Dataset()
        {
            var dataset = new Dataset
                {
                    Naam = "Kunst",
                    Beschrijving = "Bevat data over alle kunstwerken in Zoetermeer. De titel, geo-locatie, naam v. kunstenaar, materiaal en jaar staan erin beschreven. Ook staan er links naar de betreffende pagina van de website van Gemeente Zoetermeer, met gedetailleerde informatie en foto's."
                };
            ViewBag.Message = "Dataset pagina";

            return View(dataset);
        }

        public ActionResult DatasetBewerken()
        {
            var dataset = new Dataset
                {
                    Naam = "Kunst",
                    Beschrijving = "Bevat data over alle kunstwerken in Zoetermeer. De titel, geo-locatie, naam v. kunstenaar, materiaal en jaar staan erin beschreven. Ook staan er links naar de betreffende pagina van de website van Gemeente Zoetermeer, met gedetailleerde informatie en foto's."
                };
            ViewBag.Message = "Dataset Bewerken";

            return View(dataset);
        }

        public ActionResult Domeinen()
        {
            ViewBag.Message = "Domeinen";

            return View();
        }

        public ActionResult Datasets()
        {
            ViewBag.Message = "Datasets";

            Dataset set1 = new Dataset { Naam = "Kunst", Beschrijving = "Dataset over kunst" };
            Dataset set2 = new Dataset { Naam = "Bomen", Beschrijving = "Dataset over bomen" };

            List<Dataset> datasets = new List<Dataset> { set1, set2 };
            DatasetsViewModel datasetsViewModel = new DatasetsViewModel { Datasets = datasets };

            return View(datasetsViewModel);
        }
    }
}