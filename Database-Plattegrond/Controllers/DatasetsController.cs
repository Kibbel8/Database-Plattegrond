using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Database_Plattegrond.Models;
using Database_Plattegrond.DatabaseService;

namespace Database_Plattegrond.Controllers
{
    public class DatasetsController : Controller
    {
        // GET: Dataset
        public ActionResult Index()
        {
            ViewBag.Message = "Datasets";

            Dataset set1 = new Dataset { Naam = "Kunst", Beschrijving = "Dataset over kunst" };
            Dataset set2 = new Dataset { Naam = "Bomen", Beschrijving = "Dataset over bomen" };

            List<Dataset> datasets = new List<Dataset> { set1, set2 };
            DatasetsViewModel datasetsViewModel = new DatasetsViewModel { Datasets = datasets };

            return View(datasetsViewModel);
        }

<<<<<<< HEAD:Database-Plattegrond/Controllers/DatasetController.cs
        public ActionResult Detail()
=======
        public ActionResult Details(int? id = -1)
>>>>>>> 974b949f019bc28a74b4247cd8bf8dfc250cedc4:Database-Plattegrond/Controllers/DatasetsController.cs
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

<<<<<<< HEAD:Database-Plattegrond/Controllers/DatasetController.cs
        public ActionResult Toevoegen()
        {
            ViewBag.Message = "Dataset Pagina toevoegen";

            return View();
        }

=======
        [HttpPost]
        public ActionResult DatasetBewerken(Dataset model)
        {
            ViewBag.Message = "Dataset Bewerken";

            DatasetsDatabaseService dds = new DatasetsDatabaseService();
            int rowsAffected = dds.UpdateDataset(model);
            return View(model);
        }
>>>>>>> 974b949f019bc28a74b4247cd8bf8dfc250cedc4:Database-Plattegrond/Controllers/DatasetsController.cs
    }
}