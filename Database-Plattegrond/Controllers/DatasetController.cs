using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Database_Plattegrond.Models;

namespace Database_Plattegrond.Controllers
{
    public class DatasetController : Controller
    {
        // GET: Dataset
        public ActionResult Random()
        {
            var dataset = new Dataset()
            {
                Naam = "Kunst",
                Beschrijving = "Bevat data over alle kunstwerken in Zoetermeer. De titel, geo-locatie, naam v. kunstenaar, materiaal en jaar staan erin beschreven.Ook staan er links naar de betreffende pagina van de website van Gemeente Zoetermeer, met gedetailleerde informatie en foto's.",
                //Applicatie = "Schilderijen en kunstobjecten",
                //Database = "Openbare kunstobjecten"
            };
            return View(dataset);
        }
    }
}