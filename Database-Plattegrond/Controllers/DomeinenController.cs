﻿using Database_Plattegrond.DatabaseService;
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

            DomeinenDatabaseService DDS = new DomeinenDatabaseService();
            DomeinenViewModel DVM = new DomeinenViewModel { Domeinen = DDS.GetHoofdDomeinen() };

            return View(DVM);
        }


        public ActionResult DomeinenBewerken(string naam)
        {
            DomeinenDatabaseService DDS = new DomeinenDatabaseService();
            Domein domein = DDS.GetDomeinFromNaam(naam);
            return View(domein);
        }

        [HttpPost]
        public ActionResult DomeinenBewerken(Domein domein)
        {
            DomeinenDatabaseService DDS = new DomeinenDatabaseService();
            DDS.UpdateDomein(domein);
            return View(domein);
        }

        public ActionResult Toevoegen()
        {
            ViewBag.Message = "Domein toevoegen";

            return View();
        }

        [HttpPost]
        public ActionResult Toevoegen(Domein domein)
        {
            DomeinenDatabaseService DDS = new DomeinenDatabaseService();
            DDS.InsertDomein(domein);

            return RedirectToAction("DomeinenBewerken", new { naam = domein.Naam});
        }
    }
}