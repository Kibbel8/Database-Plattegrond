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
        public ActionResult Index(string domein = "")
        {
            ViewBag.Message = "Datasets";
            DatasetsViewModel datasetsViewModel = new DatasetsViewModel()
            {
                Domein = domein
            };

            DatasetsDatabaseService dds = new DatasetsDatabaseService();
            if (domein == "")
            {
                datasetsViewModel.Datasets = dds.GetAllDatasets();
            }
            else
            {
                datasetsViewModel.Datasets = dds.GetDatasetsVoorDomein(domein);
            }

            DomeinenDatabaseService domDS = new DomeinenDatabaseService();
            datasetsViewModel.Domeinen = domDS.GetHoofdDomeinen();

            return View(datasetsViewModel);
        }

        public ActionResult Details(int? id = -1)
        {
            ViewBag.Message = "Dataset pagina";

            DatasetsDatabaseService DDS = new DatasetsDatabaseService();
            Dataset dataset = DDS.GetDatasetFromId(id.Value);

            CommentDatabaseService CDS = new CommentDatabaseService();
            //TODO Sorteer comments op logische wijze
            List<Comment> comments = CDS.GetCommentsVoorDataset(id.Value);

            RelevantDatabaseService RDS = new RelevantDatabaseService();
            List<Relevant> links = RDS.GetRelevanteLinksVoorDataset(id.Value);

            DatasetDetail datasetDetail = new DatasetDetail { Dataset = dataset, Comments = comments, Links = links};
            return View(datasetDetail);
        }

        [HttpPost]
        public ActionResult SubmitComment(DatasetDetail datasetDetail)
        {
            CommentDatabaseService CDS = new CommentDatabaseService();

            Comment comment = new Comment
            {
                DatasetID = datasetDetail.Dataset.Id,
                DatumGeplaatst = DateTime.Now,
                Status = "Niet verwerkt",
                Tekst = datasetDetail.NewCommentText,
                Gebruiker = new Gebruiker { ID = 0 }
            };

            CDS.InsertComment(comment);

            return RedirectToAction("Details", new { id = datasetDetail.Dataset.Id });
        }

        public ActionResult Bewerken(int? id = -1)
        {
            ViewBag.Message = "Dataset Bewerken";

            DatasetsDatabaseService dds = new DatasetsDatabaseService();
            Dataset dataset = dds.GetDatasetFromId(id.Value);

            DomeinenDatabaseService domeinDatabaseService = new DomeinenDatabaseService();
            List<Domein> domeinen = domeinDatabaseService.GetAlleDomeinen();

            List<Domein> domeinenVoorDataset = domeinDatabaseService.GetDomeinenVoorDataset(id.Value);

            foreach (Domein datasetDomein in domeinenVoorDataset)
            {
                bool contains = domeinenVoorDataset.Any(domein => domein.Naam == datasetDomein.Naam);
                if (contains)
                {
                    int location = domeinen.FindIndex(domein => domein.Naam == datasetDomein.Naam);
                    domeinen[location].Selected = true;
                }
            }

            DatasetBewerken datasetBewerken = new DatasetBewerken
            {
                Dataset = dataset,
                TypeDatasets = new List<SelectListItem> { { new SelectListItem { Text = "Test 1", Value = "Test 1" } }, { new SelectListItem { Text = "Test 2", Value = "Test 2" } } },
                Domeinen = domeinen
            };

            return View(datasetBewerken);
        }

        [HttpPost]
        public ActionResult Bewerken(DatasetBewerken model)
        {
            ViewBag.Message = "Dataset Bewerken";

            DatasetsDatabaseService dds = new DatasetsDatabaseService();
            int rowsAffected = dds.UpdateDataset(model.Dataset);
            return RedirectToAction("Details", new { id = model.Dataset.Id });
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
            int id = dds.InsertDataset(model);

            return RedirectToAction("Details", new { id = id });
        }

        public ActionResult AanvraagFormulier(int? id = -1)
        {
            ViewBag.Message = "Aanvraag Formulier";
            
            DatasetsDatabaseService dds = new DatasetsDatabaseService();
            Dataset dataset = dds.GetDatasetFromId(id.Value);

            Aanvragen aanvragen = new Aanvragen
            {
                Naam = dataset.Naam,
                Beschrijving = dataset.Beschrijving,
                Eigenaar = dataset.Eigenaar
            };

            return View(aanvragen);
        }
    }
}