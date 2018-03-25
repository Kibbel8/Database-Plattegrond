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
    public class DatasetsController : Controller
    {
        // GET: Dataset
        public ActionResult Index()
        {
            ViewBag.Message = "Datasets";
            DatasetsDatabaseService dds = new DatasetsDatabaseService();
            DatasetsViewModel datasetsViewModel = dds.GetAllDatasets();


            return View(datasetsViewModel);
        }

        public ActionResult Details(int? id = -1)
        {
            ViewBag.Message = "Dataset pagina";

            DatasetsDatabaseService dds = new DatasetsDatabaseService();
            Dataset dataset = dds.GetDatasetFromId(id.Value);

            return View(dataset);
        }

        public ActionResult DatasetBewerken(int? id = -1)
        {
            ViewBag.Message = "Dataset Bewerken";

            DatasetsDatabaseService dds = new DatasetsDatabaseService();
            Dataset dataset = dds.GetDatasetFromId(id.Value);

            return View(dataset);
        }
        [HttpPost]
        public ActionResult DatasetBewerken(Dataset model)
        {
            ViewBag.Message = "Dataset Bewerken";

            DatasetsDatabaseService dds = new DatasetsDatabaseService();
            int rowsAffected = dds.UpdateDataset(model);
            return RedirectToAction("Details", new { id = model.Id });
        }

        public ActionResult Toevoegen()
        {
            ViewBag.Message = "Dataset Pagina toevoegen";

            return View();
        }

        [HttpPost]
        public ActionResult Toevoegen(Dataset model)
        {
            ViewBag.Message = "Dataset Pagina toevoegen";
            model.DatumAangemaakt = DateTime.Now;

            DatasetsDatabaseService dds = new DatasetsDatabaseService();
            model.Id = dds.InsertDataset(model);

            return RedirectToAction("Details", new { id = model.Id });
        }

        public ActionResult AanvraagFormulier()
        {
            ViewBag.Message = "Aanvraag Formulier";

            return View();
        }
    }
}