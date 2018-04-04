using Database_Plattegrond.DatabaseService;
using Database_Plattegrond.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Database_Plattegrond.Controllers
{
    //[Authorize(Roles = "Beheerder")]
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

        public ActionResult SoortenDatasets()
        {
            SoortDatasetDatabaseService soortDatasetDB = new SoortDatasetDatabaseService();
            SoortDatasetsViewModel soortDatasetViewModel = new SoortDatasetsViewModel { SoortDatasets = soortDatasetDB.GetAlleSoortDataset() };
            return View(soortDatasetViewModel);
        }

        public ActionResult SoortenDatasetsToevoegen(string type)
        {
            return View();
        }

        [HttpPost]
        public ActionResult SoortenDatasetsToevoegen(SoortDataset soortDataset)
        {
            SoortDatasetDatabaseService soortDatasetDB = new SoortDatasetDatabaseService();
            soortDatasetDB.InsertSoortDataset(soortDataset);
            return RedirectToAction("SoortenDatasetsBewerken", new { type = soortDataset.Type });
        }

        public ActionResult SoortenDatasetsBewerken(string type)
        {
            SoortDatasetDatabaseService soortDatasetDB = new SoortDatasetDatabaseService();
            SoortDataset soortDataset = soortDatasetDB.GetSoortDataset(type);
            return View(soortDataset);
        }

        [HttpPost]
        public ActionResult SoortenDatasetsBewerken(SoortDataset soortDataset)
        {
            SoortDatasetDatabaseService soortDatasetDB = new SoortDatasetDatabaseService();
            soortDatasetDB.UpdateSoortDataset(soortDataset);
            return View(soortDataset);
        }
    }
}